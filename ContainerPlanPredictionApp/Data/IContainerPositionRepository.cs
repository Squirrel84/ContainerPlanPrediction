using ContainerPlanPrediction.Models;
using ContainerPlanPrediction.Services;
using System.Collections.Generic;

namespace ContainerPlanPrediction.Data
{
    public interface IContainerPositionRepository
    {
        void Save(IEnumerable<Drop<ContainerDestination>> containerPositions);
    }
}
