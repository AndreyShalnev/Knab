using System;
using System.Text.Json.Serialization;
using Knab.Data;

namespace Knab.CoinMarketCapClient.Data
{
    public class Quote
    {
        public CurrencyType Currency { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        
        [JsonPropertyName("last_updated")]
        public DateTime LastUpdated { get; set; }
    }
}
