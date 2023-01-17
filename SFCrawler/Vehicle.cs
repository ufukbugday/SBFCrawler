using Newtonsoft.Json;

namespace SBFCrawler
{
    public class Vehicle
    {
        [JsonProperty("bodystyle", NullValueHandling = NullValueHandling.Ignore)]
        public string Bodystyle { get; set; }

        [JsonProperty("cpo_indicator", NullValueHandling = NullValueHandling.Ignore)]
        public bool CpoIndicator { get; set; }
   
        [JsonProperty("customer_id", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerId { get; set; }

        [JsonProperty("horizontal_position", NullValueHandling = NullValueHandling.Ignore)]
        public string HorizontalPosition { get; set; }
    
        [JsonProperty("listing_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ListingId { get; set; }
    
        [JsonProperty("make", NullValueHandling = NullValueHandling.Ignore)]
        public string Make { get; set; }
    
        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }
    
        [JsonProperty("model_year", NullValueHandling = NullValueHandling.Ignore)]
        public int ModelYear { get; set; }

        [JsonProperty("results_page_number", NullValueHandling = NullValueHandling.Ignore)]
        public int ResultsPageNumber { get; set; }

        [JsonProperty("msrp", NullValueHandling = NullValueHandling.Ignore)]
        public string Mrsp { get; set; }

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public string Price { get; set; }
    
        [JsonProperty("sponsored?", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Sponsored { get; set; }
    
        [JsonProperty("stock_type", NullValueHandling = NullValueHandling.Ignore)]
        public string StockType { get; set; }
    
        [JsonProperty("trim", NullValueHandling = NullValueHandling.Ignore)]
        public string Trim { get; set; }
    
        [JsonProperty("vertical_position", NullValueHandling = NullValueHandling.Ignore)]
        public int VerticalPosition { get; set; }
    
        [JsonProperty("web_page_type_from", NullValueHandling = NullValueHandling.Ignore)]
        public string WebPageTypeForm { get; set; }
    }
}