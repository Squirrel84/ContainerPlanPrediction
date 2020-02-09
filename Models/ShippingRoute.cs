using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerPlanPrediction.Models
{
    public class ShippingRoute
    {
        private readonly IOrderedEnumerable<KeyValuePair<int, Port>> _ports;
        private readonly List<ContainerDestination> _containerRoutes;

        public ShippingRoute(IEnumerable<KeyValuePair<int, Port>> ports, IEnumerable<ContainerDestination> containerRoutes)
        {
            _ports = ports.OrderBy(x => x.Key).ThenBy(x => x.Value.Name);
            _containerRoutes = containerRoutes.ToList();
        }

        public Port GetNextDestination()
        {
            Port nextDestination = null;
            if (CurrentLocation == null)
            {
                return _ports.First().Value;
            }

            int currentRegionNumber = _ports.First(x => x.Value == CurrentLocation).Key;
            bool portsInThisRegion = _ports.Any(x => x.Key == currentRegionNumber && x.Value != CurrentLocation);

            if (portsInThisRegion)
            {
                nextDestination = getNextDestinationFromCurrentRegion(currentRegionNumber);
            }

            if(nextDestination == null)
            {
                //go to next region
                int nextRegionNumber = currentRegionNumber + 1;
                nextDestination = _ports.First(x => x.Key == nextRegionNumber).Value;
            }

            return nextDestination;
        }

        private Port getNextDestinationFromCurrentRegion(int regionNumber)
        {
            Port nextDestination = null;
            var portsInRegion = _ports.Where(x => x.Key == regionNumber).ToList();

            var currentRegionIndex = portsInRegion.FindIndex(kvp => kvp.Value == CurrentLocation);

            if (currentRegionIndex < portsInRegion.Count - 1)
            {
                int nextRegionIndex = currentRegionIndex + 1;
                nextDestination = portsInRegion.Skip(nextRegionIndex).Take(1).First().Value;
            }

            return nextDestination;
        }

        public void ArrivedAtDestination()
        {
            var port = GetNextDestination();

            CurrentLocation = _ports.First(x => x.Value == port).Value;
        }

        public Port CurrentLocation { get; set; }
        public bool Complete { get { return CurrentLocation == _ports.Last().Value; } }

        public IEnumerable<Container> DischargeList()
        {
            var containersToRemove = _containerRoutes.Where(cr => cr.Destination == CurrentLocation).Select(cr => cr.Container).ToArray();

            foreach (var containerToRemove in containersToRemove)
            {
                _containerRoutes.Remove(_containerRoutes.First(x => x.Container == containerToRemove));
            }

            return containersToRemove;
        }

        public IEnumerable<ContainerDestination> GetAllOnboardContainers()
        {
            return _containerRoutes;
        }

        public IEnumerable<Container> RemainingContainers()
        {
            return _containerRoutes.Select(x => x.Container);
        }

        public IEnumerable<Port> UnvisitedContainerLocations()
        {
            return _containerRoutes.Select(x => x.Destination).Distinct();
        }
    }
}
