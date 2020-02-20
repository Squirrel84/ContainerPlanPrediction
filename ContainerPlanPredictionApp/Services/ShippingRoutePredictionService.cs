using ContainerPlanPrediction.Models;
using ContainerPlanPredictionML.Model;
using System;

namespace ContainerPlanPrediction.Services
{
    public class ShippingRoutePredictionService
    {
        ConsumeModel ConsumeModel = null;
        public ShippingRoutePredictionService()
        {
            string currentDirectory = System.Environment.CurrentDirectory;
            var modelDirectory = currentDirectory.Substring(0, currentDirectory.LastIndexOf("\\")) + "\\ContainerPlanPredictionML.Model\\";

            ConsumeModel.Initialise(modelDirectory);
            ConsumeModel = ConsumeModel.CurrentModel;
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
            ModelOutput result = ConsumeModel.PredictRow(input);

            return result.Prediction.ToString();
        }

        public string PredictBay(ContainerDestination containerDestination, int row)
        {
            // Add input data
            var input = new ModelInput
            {
                Row = row,
                ContainerContentType = containerDestination.Container.ContentType.ToString(),
                ContainerSize = containerDestination.Container.Size.ToString(),
                ContainerType = containerDestination.Container.ContainerType.ToString(),
                RouteNumber = containerDestination.Destination.RegionNumber
            };

            // Load model and predict output of sample data
            ModelOutput result = ConsumeModel.PredictBay(input);

            return result.Prediction.ToString();
        }
    }
}
