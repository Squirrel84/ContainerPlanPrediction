using System;
using System.Collections.Generic;
using ContainerPlanPrediction.Models;
using ContainerPlanPrediction.Services;

namespace ContainerPlanPrediction.Data
{
    public class ContainerPositionRepository : IContainerPositionRepository
    {
        private readonly ShipPlanningContext Context;
        public ContainerPositionRepository(ShipPlanningContext shipPlanningContext)
        {
            Context = shipPlanningContext;
        }

        public void Save(IEnumerable<Drop<ContainerDestination>> containerPositions)
        {
            Guid scenarioId = Guid.NewGuid();

            foreach (var containerPosition in containerPositions)
            {
                Context.ContainerPositions.Add(new ContainerPositionEntity()
                {
                    Scenario = scenarioId,
                    Row = containerPosition.Row,
                    Bay = containerPosition.Bay,
                    ContainerContentType = containerPosition.Data.Container.ContentType.ToString(),
                    ContainerSize = containerPosition.Data.Container.Size.ToString(),
                    ContainerType = containerPosition.Data.Container.ContainerType.ToString(),
                    RouteNumber = containerPosition.Data.Destination.RegionNumber.ToString()
                });
            }

            Context.SaveChanges();
        }
    }
}
