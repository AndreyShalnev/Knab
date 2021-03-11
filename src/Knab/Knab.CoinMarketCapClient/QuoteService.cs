using System.Collections.Generic;
using System.Linq;
using Knab.Data;
using Knab.Data.Provider;

namespace Knab.CoinMarketCapClient
{
    public class QuoteService : IQuoteService
    {
        private ICoinMarketCapClient _client;

        private CurrencyType[] _requestedCurrencyTypes =
            {CurrencyType.USD, CurrencyType.EUR, CurrencyType.BRL, CurrencyType.GBP, CurrencyType.AUD};

        public QuoteService(ICoinMarketCapClient client)
        {
            _client = client;
        }

        public IEnumerable<Quote> GetQuotes(string cryptoCode)
        {
            var quotes = _client.GetQuotes(cryptoCode, _requestedCurrencyTypes).Result;
            return quotes.Select(i => new Quote
            {
                Currency = i.Currency,
                Price = i.Price,
                LastUpdated = i.LastUpdated
            });
        }
    }
}
