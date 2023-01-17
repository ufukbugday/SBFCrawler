using CefSharp.OffScreen;
using Microsoft.Extensions.Configuration;
using SBFCrawler.Model;
using SBFCrawler.Services;

namespace SBFCrawler.Shared.Helpers
{
    public class ProgramHelper
    {
        private static IConfiguration _configuration;

        public static async Task<Result> GetTeslaData(IChromiumWebBrowserService chromiumWebBrowserService, ChromiumWebBrowser browser, string model, bool forTeslaModelS)
        {
            var builder = new ConfigurationBuilder().AddJsonFile($"appSettings.json", true, true);
            _configuration = builder.Build();

            var twoPageVehicleData = new List<Vehicle>();

            await SearchTeslaModelWithCriteria(chromiumWebBrowserService, browser, model);

            WaitForRendering();

            await GetPageDataForTeslaModel(chromiumWebBrowserService, browser, twoPageVehicleData);

            WaitForRendering();

            await GotoNextPage(chromiumWebBrowserService, browser);

            WaitForRendering();

            await GetPageDataForTeslaModel(chromiumWebBrowserService, browser, twoPageVehicleData);

            WaitForRendering();

            ChooseRandomCarFromFirstTwoPage(twoPageVehicleData, browser, _configuration["Domain:LoginUrl"]);

            WaitForRendering();

            var specificVehicleDetail = await GetRandomlyPickedVehicleData(chromiumWebBrowserService, browser);

            WaitForRendering();

            GotoHomepage(browser, _configuration["Domain:Homepage"]);

            WaitForRendering();

            await SearchTeslaModelWithCriteria(chromiumWebBrowserService, browser, model);

            WaitForRendering();

            await FilterResultByHomeDelivery(chromiumWebBrowserService, browser);

            WaitForRendering();

            var vehicleHomeDeliveryList = await GetHomeDeliveryFilterList(chromiumWebBrowserService, browser);

            var result = new Result();
            if (forTeslaModelS)
            {
                result.TwoPageDataTeslaModelS = twoPageVehicleData;
                result.SpecificVehicleDetailTeslaModelS = specificVehicleDetail;
                result.HomeDeliveryVehiclesTeslaModelS = vehicleHomeDeliveryList;
            }
            else
            {
                result.TwoPageDataTeslaModelX = twoPageVehicleData;
                result.SpecificVehicleDetailTeslaModelX = specificVehicleDetail;
                result.HomeDeliveryVehiclesTeslaModelx = vehicleHomeDeliveryList;
            }

            return result;
        }

        public static async Task<List<Vehicle>> GetHomeDeliveryFilterList(IChromiumWebBrowserService chromiumWebBrowserService,
            ChromiumWebBrowser browser) => await chromiumWebBrowserService.GetVehicles(browser);

        public static async Task FilterResultByHomeDelivery(IChromiumWebBrowserService chromiumWebBrowserService,
            ChromiumWebBrowser browser) => await chromiumWebBrowserService.FilterByHomeDelivery(browser);

        public static void GotoHomepage(ChromiumWebBrowser browser, string? homepage) => browser.LoadUrl(homepage);

        public static async Task<VehicleDetail> GetRandomlyPickedVehicleData(IChromiumWebBrowserService chromiumWebBrowserService,
            ChromiumWebBrowser browser) => await chromiumWebBrowserService.GetVehicleDetail(browser);

        public static void ChooseRandomCarFromFirstTwoPage(List<Vehicle> first2PageVehicleData, ChromiumWebBrowser browser,
            string? vehicleDetailBaseUrl)
        {
            var rnd = new Random();
            var randomCar = rnd.Next(0, first2PageVehicleData.Count - 1);
            browser.LoadUrl(vehicleDetailBaseUrl + first2PageVehicleData[randomCar].ListingId);
        }

        public static async Task GetPageDataForTeslaModel(IChromiumWebBrowserService chromiumWebBrowserService,
            ChromiumWebBrowser browser, List<Vehicle> first2PageVehicleData) => first2PageVehicleData.AddRange(await chromiumWebBrowserService.GetVehicles(browser));

        public static async Task GotoNextPage(IChromiumWebBrowserService chromiumWebBrowserService, ChromiumWebBrowser browser) => await chromiumWebBrowserService.GotoNextPage(browser);

        public static async Task SearchTeslaModelWithCriteria(IChromiumWebBrowserService chromiumWebBrowserService,
            ChromiumWebBrowser browser, string teslaModel) => await chromiumWebBrowserService.SearchVehicles(browser, CommonHelper.GetTesla_S_ModelSearchCriteria(teslaModel));

        public static void WaitForRendering() => Thread.Sleep(5000);

        public static async Task LoginToSystem(IChromiumWebBrowserService chromiumWebBrowserService, ChromiumWebBrowser browser) => await chromiumWebBrowserService.Login(browser);
    }
}
