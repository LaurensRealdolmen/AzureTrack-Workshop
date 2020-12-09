using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RMotownFestival.Functions
{
    public class AnalyzationFunction
    {
        private readonly ComputerVisionClient _computerVisionClient;
        private static readonly List<VisualFeatureTypes?> Features = new List<VisualFeatureTypes?> { VisualFeatureTypes.Adult };

        public AnalyzationFunction(ComputerVisionClient computerVisionClient)
        {
            _computerVisionClient = computerVisionClient;
        }

        [FunctionName("AnalyzationFunction")]
        public async Task Run([BlobTrigger("picsin/{name}", Connection = "BlobStorageConnection")] byte[] image,
           string name,
           ILogger log,
           Binder binder)
        {
            log.LogInformation($"C# AnalyzationFunction trigger function Processed blob\n Name:{name} \n Size: {image.Length} Bytes");

            ImageAnalysis analysis = await _computerVisionClient.AnalyzeImageInStreamAsync(new MemoryStream(image), Features);

            Attribute[] attributes;
            if (analysis.Adult.IsAdultContent || analysis.Adult.IsGoryContent || analysis.Adult.IsRacyContent)
            {
                attributes = new Attribute[]
                {
                    new BlobAttribute($"picsrejected/{name}", FileAccess.Write),
                    new StorageAccountAttribute("BlobStorageConnection")
                };
            }
            else
            {
                attributes = new Attribute[]
                {
                    new BlobAttribute($"festivalpics/{name}", FileAccess.Write),
                    new StorageAccountAttribute("BlobStorageConnection")
                };
            }

            using Stream fileOutputStream = await binder.BindAsync<Stream>(attributes);
            fileOutputStream.Write(image);
        }
    }
}
