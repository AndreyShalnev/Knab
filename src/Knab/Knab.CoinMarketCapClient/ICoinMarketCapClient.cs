using System.Collections.Generic;
using System.Threading.Tasks;
using Knab.Data;

namespace Knab.CoinMarketCapClient
{
    public interface ICoinMarketCapClient
    {
        Task<IEnumerable<Data.Quote>> GetQuotes(string cryptoCode, CurrencyType[] currencyCodes);
    }
}
