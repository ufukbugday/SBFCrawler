using CefSharp;
using CefSharp.OffScreen;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SBFCrawler.Model;
using SBFCrawler.Shared.Extensions;
using SBFCrawler.Shared.Helpers;

namespace SBFCrawler.Services
{
    public class ChromiumWebBrowserService : IChromiumWebBrowserService
    {
        private readonly IConfiguration _configuration;
        public ChromiumWebBrowserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<JavascriptResponse> Login(ChromiumWebBrowser browser)
        {
            var configLogin = _configuration.GetLoginDataFromConfig();

            var loginScript = LoginHelper.GetLoginScript(configLogin);

            return await browser.EvaluateScriptAsync(loginScript);

        }

        public async Task<List<Vehicle>> GetVehicles(ChromiumWebBrowser browser)
        {
            var response = await GetVehiclesResponse(browser);

            dynamic result = response.Result;

            var vehicles = new List<Vehicle>();

            foreach (var obj in result)
            {
                if (obj != null)
                {
                    vehicles.Add(JsonConvert.DeserializeObject<Vehicle>(obj, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    }));
                }
            }

            return vehicles;
        }

        public async Task<VehicleDetail> GetVehicleDetail(ChromiumWebBrowser browser)
        {
            var scriptFirstCarDetail = CommonHelper.GetFirstCarDetailScript();
            var jsonSerializeSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var responseFirstCarDetail = await browser.EvaluateScriptAsync(scriptFirstCarDetail);
            var firstCarDetailJson = JsonConvert.SerializeObject(responseFirstCarDetail.Result, jsonSerializeSettings);

            return JsonConvert.DeserializeObject<VehicleDetail>(firstCarDetailJson, jsonSerializeSettings) ?? new VehicleDetail();
        }

        private static async Task<JavascriptResponse> GetVehiclesResponse(ChromiumWebBrowser browser)
        {
            var getVehiclesScript = VehicleHelper.GetVehiclesScript();
            return await browser.EvaluateScriptAsync(getVehiclesScript);
        }

        public async Task<JavascriptResponse> SearchVehicles(ChromiumWebBrowser browser, SearchCriteria searchCriteria)
        {
            var getSearchVehiclesScript = VehicleHelper.GetSearchVehiclesScript(searchCriteria);
            var result = await browser.EvaluateScriptAsync(getSearchVehiclesScript);
            return result;
        }

        public async Task<JavascriptResponse> GotoNextPage(ChromiumWebBrowser browser)
        {
            var goNextPage = CommonHelper.GetGotoNextPageScript();
            return await browser.EvaluateScriptAsync(goNextPage);
        }

        public async Task<JavascriptResponse> FilterByHomeDelivery(ChromiumWebBrowser browser)
        {
            var homeDeliveryCheck = CommonHelper.GetHomeDeliveryScript();
            return await browser.EvaluateScriptAsync(homeDeliveryCheck);
        }
    }
}