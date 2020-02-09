using ContainerPlanPrediction.Models;

namespace ContainerPlanPrediction.Data
{
    public class ShippingRouteService
    {
        private readonly ShippingScenarioSetup scenarioSetup = null;

        public ShippingRouteService(ShippingScenarioSetup scenarioSetup)
        {
            this.scenarioSetup = scenarioSetup;
        }

        public ShippingRoute GetShippingRoute()
        {
            return this.scenarioSetup.CreateRandomShippingRoute();
        }
    }
}
