using ContainerPlanPrediction.Models;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.ML.DataOperationsCatalog;

namespace ContainerPlanPrediction.Data
{
    public class ShippingRoutePredictionService
    {
        MLContext mlContext = null;

        public ShippingRoutePredictionService(IEnumerable<Drop<ContainerDestination>> containerPositions)
        {
            mlContext = new MLContext(seed: 0);
            //IDataView dataCollatedSoFar = mlContext.Data.LoadFromEnumerable<Drop<ContainerDestination>>(containerPositions);

            //var rowEnumerable = mlContext.Data.CreateEnumerable<Drop<ContainerDestination>>(dataCollatedSoFar, reuseRowObject: true);

            //TrainTestData trainTestData = mlContext.Data.TrainTestSplit(dataCollatedSoFar, testFraction: 0.2, seed: 1);
            //IDataView trainData = trainTestData.TrainSet;
            //IDataView testData = trainTestData.TestSet;


        }
    }
}
