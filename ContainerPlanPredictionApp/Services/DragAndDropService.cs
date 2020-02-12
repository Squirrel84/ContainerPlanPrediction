using ContainerPlanPrediction.Data;
using ContainerPlanPrediction.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerPlanPrediction.Services
{
    public class DragAndDropService<T>
    {
        public List<Drop<T>> Drops { get; set; }

        public DragAndDropService()
        {
            Drops = new List<Drop<T>>();
        }

        public T Data { get; set; }

        public void StartDrag(T data)
        {
            this.Data = data;
        }

        public virtual bool Accepts(T data, string row, string bay)
        {
            return true;
        }

        public virtual void OnDrop(T data, string row, string bay)
        {            
            UpdateDrops(new Drop<T>(data, row, bay));
        }

        protected virtual void UpdateDrops(Drop<T> drop)
        {
            RemoveExisting(drop);

            Drops.Add(drop);
        }

        protected virtual void RemoveExisting(Drop<T> drop)
        {
            var existingDrop = Drops.FirstOrDefault(x => x.Data.GetHashCode() == drop.Data.GetHashCode());
            var existingPosition = Drops.FirstOrDefault(x => x.Bay == drop.Bay && x.Row == drop.Row);
            if (existingPosition != null)
            {
                Drops.Remove(existingPosition);
            }
            if (existingDrop != null)
            {
                Drops.Remove(existingDrop);
            }
        }
    }

    public class Drop<T>
    {
        public Drop()
        { 
        
        }

        public Drop(T data, string row, string bay)
        {
            Data = data;
            Row = row;
            Bay = bay;
        }

        public T Data { get; set; }
        public string Row { get; set; }
        public string Bay { get; set; }
    }


    public class ContainerPlannerService : DragAndDropService<ContainerDestination>
    {
        private readonly IContainerPositionRepository containerPositionRepository;
        public ContainerPlannerService(IContainerPositionRepository containerPositionRepository)
        {
            this.containerPositionRepository = containerPositionRepository;
        }

        public override bool Accepts(ContainerDestination data, string row, string bay)
        {
            var isFortyFoot = data.Container.Size == ContainerSize.FortyFoot;
            if (isFortyFoot)
            {
                if (bay == "3")
                {
                    return false;
                }

                //if (bay != "2")
                //{
                //    var middleSpaceContent = Drops.FirstOrDefault(x => x.Row == row && x.Bay == "2");
                //    if (middleSpaceContent != null)
                //    {
                //        return false;
                //    }
                //}
            }

            return true;
        }

        protected override void UpdateDrops(Drop<ContainerDestination> drop)
        {
            base.UpdateDrops(drop);

            if (drop.Data.Container.Size == ContainerSize.FortyFoot)
            {
                Drops.Add(new Drop<ContainerDestination>(drop.Data, drop.Row, (int.Parse(drop.Bay) + 1).ToString()));
            }
        }

        protected override void RemoveExisting(Drop<ContainerDestination> drop)
        {
            var existingPosition = Drops.FirstOrDefault(x => x.Bay == drop.Bay && x.Row == drop.Row);
            if (existingPosition != null)
            {
                Drops.Remove(existingPosition);

                if (existingPosition.Data.Container.Size == ContainerSize.FortyFoot)
                {
                    string rowNumber = existingPosition.Row;
                    string rightBayNumber = (int.Parse(existingPosition.Bay) + 1).ToString();
                    string leftBayNumber = (int.Parse(existingPosition.Bay) - 1).ToString();

                    existingPosition = Drops.FirstOrDefault(x => x.Bay == rightBayNumber && x.Row == rowNumber);
                    if (existingPosition == null)
                    {
                        existingPosition = Drops.FirstOrDefault(x => x.Bay == leftBayNumber && x.Row == rowNumber);
                    }

                    Drops.Remove(existingPosition);
                }
            }

            foreach (var objectDrop in Drops.Where(x => x.Data.GetHashCode() == drop.Data.GetHashCode()).ToArray())
            {
                Drops.Remove(objectDrop);
            }
        }

        public void Save()
        {
            containerPositionRepository.Save(this.Drops);
        }

        public void Clear()
        {
            this.Drops.Clear();
        }
    }
}
