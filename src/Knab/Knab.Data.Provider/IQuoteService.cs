using System.Collections.Generic;

namespace Knab.Data.Provider
{
    public interface IQuoteService
    {
        IEnumerable<Quote> GetQuotes(string cryptoCode);
    }
}
