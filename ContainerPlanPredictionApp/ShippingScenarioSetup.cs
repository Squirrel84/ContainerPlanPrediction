using ContainerPlanPrediction.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerPlanPrediction
{
    public class ShippingScenarioSetup
    {
        public static List<KeyValuePair<int, Port>> WorldPorts = new List<KeyValuePair<int, Port>>();
        private static Random randomPortPicker = new Random(DateTime.Now.Second);

        public ShippingScenarioSetup()
        {
            PopulateWorldPortIndex();
        }

        public ShippingRoute CreateRandomShippingRoute(int numberOfContainrs)
        {
            RandomContainerGenerater randomContainerGenerater = new RandomContainerGenerater();
            List<ContainerDestination> containerDestinations = new List<ContainerDestination>();

            for (int x = 0; x < numberOfContainrs; x++)
            {
                var container = randomContainerGenerater.GetRandomContainer();

                var destination = GetRandomDestination();

                ContainerDestination containerDestination = new ContainerDestination() { Container = container, Destination = destination };

                containerDestinations.Add(containerDestination);
            }

            var portSchedule = GeneratePortScheduleFromContainerDestinations(containerDestinations);


            return new ShippingRoute(portSchedule, containerDestinations);
        }

        private static IEnumerable<KeyValuePair<int, Port>> GeneratePortScheduleFromContainerDestinations(List<ContainerDestination> containerDestinations)
        {
            List<KeyValuePair<int, Port>> ports = new List<KeyValuePair<int, Port>>();
            var distinctPorts = containerDestinations.Select(x => x.Destination).Distinct();

            foreach (var port in distinctPorts)
            {
                int portIndex = WorldPorts.First(x => x.Value == port).Key;
                ports.Add(new KeyValuePair<int, Port>(portIndex, port));
            }

            return ports;
        }

        private static Port GetRandomDestination()
        {
            int randomPortIndex = randomPortPicker.Next(0, WorldPorts.Count - 1);
            return WorldPorts[randomPortIndex].Value;
        }

        private static void PopulateWorldPortIndex()
        {
            WorldPorts.Add(new KeyValuePair<int, Port>(1, new Port(1, "Longyearbyen")));

            WorldPorts.Add(new KeyValuePair<int, Port>(2, new Port(2, "Djupivogur")));
            WorldPorts.Add(new KeyValuePair<int, Port>(2, new Port(2, "Vestmannaeyjar")));
            WorldPorts.Add(new KeyValuePair<int, Port>(2, new Port(2, "Kopasker")));
            WorldPorts.Add(new KeyValuePair<int, Port>(2, new Port(2, "Akranes")));

            WorldPorts.Add(new KeyValuePair<int, Port>(2, new Port(2, "Kongshavn")));
            WorldPorts.Add(new KeyValuePair<int, Port>(2, new Port(2, "Thorshavn")));

            WorldPorts.Add(new KeyValuePair<int, Port>(2, new Port(2, "Nordkapp")));
            WorldPorts.Add(new KeyValuePair<int, Port>(2, new Port(2, "Vardo")));
            WorldPorts.Add(new KeyValuePair<int, Port>(2, new Port(2, "Verdal")));

            WorldPorts.Add(new KeyValuePair<int, Port>(3, new Port(3, "Ringkobing")));
            WorldPorts.Add(new KeyValuePair<int, Port>(3, new Port(3, "Hirtshals")));

            WorldPorts.Add(new KeyValuePair<int, Port>(3, new Port(3, "Berwick")));
            WorldPorts.Add(new KeyValuePair<int, Port>(3, new Port(3, "Scarborough")));
            WorldPorts.Add(new KeyValuePair<int, Port>(3, new Port(3, "Plymouth")));

            WorldPorts.Add(new KeyValuePair<int, Port>(3, new Port(3, "Wexford")));
            WorldPorts.Add(new KeyValuePair<int, Port>(3, new Port(3, "Greenore")));
            WorldPorts.Add(new KeyValuePair<int, Port>(3, new Port(3, "Baltimore")));
        }
    }
}
