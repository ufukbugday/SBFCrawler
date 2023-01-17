using SBFCrawler.Model;

namespace SBFCrawler.Shared.Helpers
{
    public static class VehicleHelper
    {
        public static string GetVehiclesScript() =>
            @"(function (){
                        var arr = Array.from(document.getElementsByClassName('vehicle-badging')).map(x => (                             
                                 x.getAttribute('data-override-payload')
                        ));
        
                        return arr;
                     })();";


        public static string GetSearchVehiclesScript(SearchCriteria searchCriteria) =>
            $@"document.querySelector('#make-model-search-stocktype').value = '{searchCriteria.StockType}';
                                    document.querySelector('#makes').value = '{searchCriteria.Makes}';
                                    document.querySelector('#models').value = '{searchCriteria.Models}';
                                    document.querySelector('#make-model-max-price').value = '{searchCriteria.Price}';
                                    document.querySelector('#make-model-maximum-distance').value = '{searchCriteria.Distance}';
                                    document.querySelector('#make-model-zip').value = {searchCriteria.Zip};
                                    document.querySelector('.sds-home-search__submit button[type=submit]').click();";
    }
}