using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RMotownFestival.Functions;

[assembly: FunctionsStartup(typeof(Startup))]
namespace RMotownFestival.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var context = builder.GetContext();

            builder.Services.AddSingleton(p => new ComputerVisionClient(new ApiKeyServiceClientCredentials(context.Configuration.GetValue<string>("ComputerVisionApiKey")))
            {
                Endpoint = context.Configuration.GetValue<string>("ComputerVisionEndpoint")
            });
        }
    }
}
