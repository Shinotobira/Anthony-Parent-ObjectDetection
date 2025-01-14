using System.Text.Json;

namespace Anthony.Parent.ObjectDetection.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                System.Console.WriteLine("Please provide the path to the directory containing the scenes.");
                return;
            }

            var scenesDirectory = args[0];
            if (!Directory.Exists(scenesDirectory))
            {
                System.Console.WriteLine($"The directory '{scenesDirectory}' does not exist.");
                return;
            }

            var imageScenesData = new List<byte[]>();
            foreach (var imagePath in Directory.EnumerateFiles(scenesDirectory))
            {
                var imageBytes = await File.ReadAllBytesAsync(imagePath);
                imageScenesData.Add(imageBytes);
            }

            var objectDetection = new ObjectDetection();
            var detectObjectInScenesResults = await objectDetection.DetectObjectInScenesAsync(imageScenesData);

            foreach (var objectDetectionResult in detectObjectInScenesResults)
            {
                System.Console.WriteLine($"Box: {JsonSerializer.Serialize(objectDetectionResult.Box)}");
            }
        }
    }
}