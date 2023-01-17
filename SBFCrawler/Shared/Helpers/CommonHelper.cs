using SBFCrawler.Model;

namespace SBFCrawler.Shared.Helpers
{
    public static class CommonHelper
    {
        public static string GetGotoNextPageScript() => @"document.querySelector('#next_paginate').click();";

        public static SearchCriteria GetTesla_S_ModelSearchCriteria(string teslaModel) => new()
        {
            StockType = "used",
            Makes = "tesla",
            Models = $"tesla-model_{teslaModel}",
            Price = 100000,
            Distance = "all",
            Zip = 94596
        };

        public static string GetFirstCarDetailScript() => @"(function (){
                        let vehicle = CARS['initialActivity']; 
                        return vehicle;
                     })();";


        public static string GetHomeDeliveryScript() => @"document.querySelector('#mobile_home_delivery_true').checked = true;";
    }
}