// This file was auto-generated by ML.NET Model Builder. 

using System;
using System.IO;
using System.Linq;
using Microsoft.ML;
using ContainerPlanPredictionML.Model;

namespace ContainerPlanPredictionML.ConsoleApp
{
    class Program
    {
        //Dataset to use for predictions 
        private const string DATA_FILENAME = @"8e851cdd-559d-4c46-b6ad-84f83823a7a8.tsv";

        static void Main(string[] args)
        {
            string currentDirectory = System.Environment.CurrentDirectory;
            string solutionDirectory = currentDirectory.Substring(0, currentDirectory.IndexOf("ContainerPlanPrediction")) + "ContainerPlanPrediction";
            var dataDirectory = solutionDirectory + "\\ContainerPlanPredictionApp\\";
            var modelDirectory = solutionDirectory + "\\ContainerPlanPredictionML.Model\\";

            var dataFilePath = Path.Combine(dataDirectory, DATA_FILENAME);

            ModelBuilder.CreateModel(dataFilePath, modelDirectory, ModelBuilder.MODEL_KEY_ROW);
            ModelBuilder.CreateModel(dataFilePath, modelDirectory, ModelBuilder.MODEL_KEY_BAY);

            // Create single instance of sample data from first line of dataset for model input
            ModelInput sampleData = CreateSingleDataSample(dataFilePath);

            ConsumeModel.Initialise(@"C:\Projects\ContainerPlanPrediction\ContainerPlanPredictionML.Model\");

            // Make a single prediction on the sample data and print results
            ModelOutput rowPredictionResult = ConsumeModel.CurrentModel.PredictRow(sampleData);

            ModelOutput bayPredictionResult = ConsumeModel.CurrentModel.PredictBay(sampleData);

            Console.WriteLine("Using model to make single prediction -- Comparing actual Row with predicted Row from sample data...\n\n");
            Console.WriteLine($"Scenario: {sampleData.Scenario}");
            Console.WriteLine($"RouteNumber: {sampleData.RouteNumber}");
            Console.WriteLine($"ContainerType: {sampleData.ContainerType}");
            Console.WriteLine($"ContainerSize: {sampleData.ContainerSize}");
            Console.WriteLine($"ContainerContentType: {sampleData.ContainerContentType}");
            Console.WriteLine($"\n\nActual Row: {sampleData.Row} \nPredicted Row value {rowPredictionResult.Prediction} \nPredicted Row scores: [{String.Join(",", rowPredictionResult.Score)}]\n\n");
            Console.WriteLine($"\n\nActual Bay: {sampleData.Bay} \nPredicted Bay value {bayPredictionResult.Prediction} \nPredicted Bay scores: [{String.Join(",", bayPredictionResult.Score)}]\n\n");
            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();
        }

        // Change this code to create your own sample data
        #region CreateSingleDataSample
        // Method to load single row of dataset to try a single prediction
        private static ModelInput CreateSingleDataSample(string dataFilePath)
        {
            // Create MLContext
            MLContext mlContext = new MLContext();

            // Load dataset
            IDataView dataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: dataFilePath,
                                            hasHeader: true,
                                            separatorChar: '\t',
                                            allowQuoting: true,
                                            allowSparse: false);

            // Use first line of dataset as model input
            // You can replace this with new test data (hardcoded or from end-user application)
            return mlContext.Data.CreateEnumerable<ModelInput>(dataView, false).First();
        }
        #endregion
    }
}
