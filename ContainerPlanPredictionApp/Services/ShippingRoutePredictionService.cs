using ContainerPlanPrediction.Models;
using ContainerPlanPredictionML.Model;

namespace ContainerPlanPrediction.Data
{
    public class ShippingRoutePredictionService
    {
        public ShippingRoutePredictionService()
        {
            
        }

        public string PredictRow(ContainerDestination containerDestination)
        {
            // Add input data
            var input = new ModelInput
            {
                ContainerContentType = containerDestination.Container.ContentType.ToString(),
                ContainerSize = containerDestination.Container.Size.ToString(),
                ContainerType = containerDestination.Container.ContainerType.ToString(),
                RouteNumber = containerDestination.Destination.RegionNumber
            };

            // Load model and predict output of sample data
            ModelOutput result = ConsumeModel.Predict(input);

            return result.Prediction.ToString();
        }
    }
}
