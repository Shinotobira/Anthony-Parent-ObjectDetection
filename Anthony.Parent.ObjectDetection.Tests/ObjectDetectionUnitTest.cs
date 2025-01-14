using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
namespace Anthony.Parent.ObjectDetection.Tests;
public class ObjectDetectionUnitTest
{
    [Fact]
    public async Task ObjectShouldBeDetectedCorrectly()
    {
        var executingPath = GetExecutingPath();
        var imageScenesData = new List<byte[]>();
        foreach (var imagePath in Directory.EnumerateFiles(Path.Combine(executingPath, "Scenes")))
        {
            var imageBytes = await File.ReadAllBytesAsync(imagePath);
            imageScenesData.Add(imageBytes);
        }
        var detectObjectInScenesResults = await new ObjectDetection().DetectObjectInScenesAsync(imageScenesData);

        Assert.Equal("[{\"Dimensions\":{\"X\":194.24707,\"Y\":99.80435,\"Height\":158.53809,\"Width\":105.60333},\"Label\":\"person\",\"Confidence\":0.49458537},{\"Dimensions\":{\"X\":289.23474,\"Y\":101.70316,\"Height\":146.01378,\"Width\":94.96253},\"Label\":\"person\",\"Confidence\":0.35916218}]",JsonSerializer.Serialize(detectObjectInScenesResults[0].Box));
        Assert.Equal("[{\"Dimensions\":{\"X\":89.50356,\"Y\":104.726105,\"Height\":264.30588,\"Width\":242.27052},\"Label\":\"chair\",\"Confidence\":0.35712093}]",JsonSerializer.Serialize(detectObjectInScenesResults[1].Box));
    }
    private static string GetExecutingPath()
    {
        var executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
        var executingPath = Path.GetDirectoryName(executingAssemblyPath);
        return executingPath;
    }
}
