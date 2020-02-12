using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContainerPlanPrediction.Data
{
    public class ContainerPositionEntity
    {
        public Guid Scenario { get; set; }
        public string RouteNumber { get; set; }
        public string Row { get; set; }
        public string Bay { get; set; }

        public string ContainerType { get; set; }
        public string ContainerSize { get; set; }
        public string ContainerContentType { get; set; }
    }
}
