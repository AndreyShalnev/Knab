using System;

namespace Knab.Data.Provider
{
    public interface IServiceConfig
    {
        string GetCoinMarketCapApiKey();
        string GetCoinMarketCapApiUrl();
        string GetCoinMarketCapQuoteApiUrl();
    }
}
