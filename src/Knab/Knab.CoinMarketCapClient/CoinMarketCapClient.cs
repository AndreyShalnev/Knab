using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Knab.CoinMarketCapClient.Helpers;
using Knab.Data;
using Knab.Data.Provider;
using Microsoft.Extensions.Logging;
using Quote = Knab.CoinMarketCapClient.Data.Quote;

namespace Knab.CoinMarketCapClient
{
    public class CoinMarketCapClient : ICoinMarketCapClient
    {
        private readonly ILogger<CoinMarketCapClient> _logger;
        private readonly HttpClient _httpClient;
        private readonly IServiceConfig _config;

        private readonly string _baseApiUrl;

        private const string DataJsonElementName = "data";
        private const string QuoteJsonElementName = "quote";

        public CoinMarketCapClient(HttpClient httpClient, IServiceConfig config, ILogger<CoinMarketCapClient> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
            _config = config;

            _baseApiUrl = _config.GetCoinMarketCapApiUrl();

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", _config.GetCoinMarketCapApiKey());
        }

        public async Task<IEnumerable<Quote>> GetQuotes(string cryptoCode, CurrencyType[] currencyCodes)
        {
            var quotes = new List<Quote>();
            
            foreach (var currency in currencyCodes)
            {
                var url = $"{_baseApiUrl}/{_config.GetCoinMarketCapQuoteApiUrl()}?symbol={cryptoCode?.ToUpper()}&convert={currency}";
                var response = await _httpClient.GetAsync(url); 
                var jsonResponse = response.Content.ReadAsStringAsync().Result;
                
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    quotes.Add(DeserializeResponse(jsonResponse, cryptoCode, currency));
                }
                else
                {
                    _logger.LogError($"Get quotes failed with code {response.StatusCode}. " +
                                     $"CryptoCode={cryptoCode}, currency={currency}. {jsonResponse}");
                    // I don't stop the process, because other currencies request can complete well.
                }
            }

            return quotes;
        }

        private Quote DeserializeResponse(string jsonResponse, string cryptoCode, CurrencyType currency)
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonDocumentConverter());
            
            var deserializedResponse = JsonSerializer.Deserialize<JsonDocument>(jsonResponse, options);
            
            var jsonCurrency = 
                    deserializedResponse?.RootElement
                        .GetProperty(DataJsonElementName)
                        .GetProperty(cryptoCode.ToUpper())
                        .GetProperty(QuoteJsonElementName)
                        .GetProperty(currency.ToString())
                        .GetRawText();
                
            var quote = JsonSerializer.Deserialize<Quote>(jsonCurrency);
            quote.Currency = currency;

            return quote;
        }
    }
}
