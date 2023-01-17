using CefSharp;
using CefSharp.OffScreen;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SBFCrawler.Services;
using SBFCrawler.Shared.Helpers;

namespace SBFCrawler
{
    public class Program
    {
        private static IConfiguration _configuration;
        private static readonly IServiceProvider Container = new ContainerBuilder(new ConfigurationBuilder().AddJsonFile($"appSettings.json", true, true).Build()).Build();
        public static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appSettings.json", true, true);
            _configuration = builder.Build();

            var loginUrl = _configuration["Domain:LoginUrl"];

            var settings = new CefSettings()
            {
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache")
            };

            var success = await Cef.InitializeAsync(settings, performDependencyCheck: true, browserProcessHandler: null);

            if (!success)
            {
                throw new Exception("InitializeAsync CEF failed.");
            }

            using var browser = new ChromiumWebBrowser(loginUrl);
            var initialLoadResponse = await browser.WaitForInitialLoadAsync();

            if (!initialLoadResponse.Success)
            {
                throw new Exception($"Initial load failed with ErrorCode:{initialLoadResponse.ErrorCode}, HttpStatusCode:{initialLoadResponse.HttpStatusCode}");
            }

            var chromiumWebBrowserService = (IChromiumWebBrowserService)Container.GetService(typeof(IChromiumWebBrowserService))!;

            await ProgramHelper.LoginToSystem(chromiumWebBrowserService, browser);

            var teslaModelSResult = await ProgramHelper.GetTeslaData(chromiumWebBrowserService, browser, "s", true);


#if DEBUG
            var teslaModelSResultJson = JsonConvert.SerializeObject(teslaModelSResult, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            });

            var savingPathTeslaModelS = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "modelS.json");

            Console.WriteLine("Json file saved to {0}", savingPathTeslaModelS);

            await File.WriteAllTextAsync(savingPathTeslaModelS, teslaModelSResultJson);
#endif

            ProgramHelper.WaitForRendering();

            var teslaModelXResult = await ProgramHelper.GetTeslaData(chromiumWebBrowserService, browser, "x", false);

#if DEBUG
            var teslaModelXResultJson = JsonConvert.SerializeObject(teslaModelXResult, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            });

            var savingPathTeslaModelX = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "modelX.json");

            Console.WriteLine("Json file saved to {0}", savingPathTeslaModelX);

            await File.WriteAllTextAsync(savingPathTeslaModelX, teslaModelXResultJson);
#endif

            var result = new Result
            {
                TwoPageDataTeslaModelS = teslaModelSResult.TwoPageDataTeslaModelS,
                SpecificVehicleDetailTeslaModelS = teslaModelSResult.SpecificVehicleDetailTeslaModelS,
                HomeDeliveryVehiclesTeslaModelS = teslaModelSResult.HomeDeliveryVehiclesTeslaModelS,
                TwoPageDataTeslaModelX = teslaModelXResult.TwoPageDataTeslaModelX,
                SpecificVehicleDetailTeslaModelX = teslaModelXResult.SpecificVehicleDetailTeslaModelX,
                HomeDeliveryVehiclesTeslaModelx = teslaModelXResult.HomeDeliveryVehiclesTeslaModelx
            };
            
            var resultJson = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            });
          
            var savingPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "result.json");

            Console.WriteLine("Json file saved to {0}", savingPath);

            await File.WriteAllTextAsync(savingPath, resultJson);

            Console.ReadLine();
            Cef.Shutdown();
        }

        
    }
}