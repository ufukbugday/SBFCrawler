using CefSharp;
using CefSharp.OffScreen;
using SBFCrawler.Model;

namespace SBFCrawler.Services
{
    public interface IChromiumWebBrowserService
    {
        Task<JavascriptResponse> Login(ChromiumWebBrowser browser);
        Task<List<Vehicle>> GetVehicles(ChromiumWebBrowser browser);
        Task<VehicleDetail> GetVehicleDetail(ChromiumWebBrowser browser);
        Task<JavascriptResponse> SearchVehicles(ChromiumWebBrowser browser, SearchCriteria searchCriteria);
        Task<JavascriptResponse> GotoNextPage(ChromiumWebBrowser browser);
        Task<JavascriptResponse> FilterByHomeDelivery(ChromiumWebBrowser browser);

    }
}