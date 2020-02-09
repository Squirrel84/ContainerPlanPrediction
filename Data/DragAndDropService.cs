using ContainerPlanPrediction.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContainerPlanPrediction.Data
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

        public void OnDrop(T data, string row, string bay)
        {
            var objectDrop = Drops.FirstOrDefault(x => x.Data.GetHashCode() == data.GetHashCode());
            var positionDrop = Drops.FirstOrDefault(x => x.Row == row && x.Bay == bay);

            RemoveDrops(objectDrop, positionDrop);

            UpdateDrops(new Drop<T>(data, row, bay));
        }

        public virtual void RemoveDrops(Drop<T> objectDrop, Drop<T> positionDrop)
        {
            if (objectDrop != null)
            {
                Drops.Remove(objectDrop);
            }

            if (positionDrop != null)
            {
                Drops.Remove(positionDrop);
            }
        }

        public virtual void UpdateDrops(Drop<T> drop)
        {
            Drops.Add(drop);
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


    public class DragDropContainerService : DragAndDropService<ContainerDestination>
    {
        public override bool Accepts(ContainerDestination data, string row, string bay)
        {
            var isFortyFoot = data.Container.Size == ContainerSize.FortyFoot;
            if (isFortyFoot)
            {
                if (bay == "3")
                {
                    return false;
                }

                if (bay != "2")
                {
                    var middleSpaceContent = Drops.FirstOrDefault(x => x.Row == row && x.Bay == "2");
                    if (middleSpaceContent != null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override void UpdateDrops(Drop<ContainerDestination> drop)
        {
            base.UpdateDrops(drop);
            if (drop.Data.Container.Size == ContainerSize.FortyFoot)
            {
                base.UpdateDrops(new Drop<ContainerDestination>(drop.Data, drop.Row, (int.Parse(drop.Bay) + 1).ToString()));   
            }
        }

        public override void RemoveDrops(Drop<ContainerDestination> objectDrop, Drop<ContainerDestination> positionDrop)
        {
            base.RemoveDrops(objectDrop, positionDrop);
            if (objectDrop != null)
            {
                if (objectDrop.Data.Container.Size == ContainerSize.FortyFoot)
                {
                    var objectPartDrop = Drops.FirstOrDefault(x => x.Row == objectDrop.Row && x.Bay == (int.Parse(objectDrop.Bay) + 1).ToString());
                    if (objectPartDrop == null)
                    {
                        objectPartDrop = Drops.FirstOrDefault(x => x.Row == objectDrop.Row && x.Bay == (int.Parse(objectDrop.Bay) - 1).ToString());
                    }

                    if (objectPartDrop != null)
                    {
                        Drops.Remove(objectPartDrop);
                    }
                }
            }

            if (positionDrop != null)
            {
                if (positionDrop.Data.Container.Size == ContainerSize.FortyFoot)
                {
                    var positionPartDrop = Drops.FirstOrDefault(x => x.Row == positionDrop.Row && x.Bay == (int.Parse(positionDrop.Bay) + 1).ToString());
                    if (positionPartDrop == null)
                    {
                        positionPartDrop = Drops.FirstOrDefault(x => x.Row == positionDrop.Row && x.Bay == (int.Parse(positionDrop.Bay) - 1).ToString());
                    }

                    if (positionPartDrop != null)
                    {
                        Drops.Remove(positionPartDrop);
                    }
                }
            }
        }
    }
}
