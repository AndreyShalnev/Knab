namespace Knab.Data.Provider
{
    // This class is just a mock for test and current version of web app.
    // But we can simply realize new class and get this data from DB or web app config file.
    public class ServiceConfig : IServiceConfig
    {
        public string GetCoinMarketCapApiKey()
        {
            return "e3e0ad4e-e04c-4884-b0c4-b2e71877f4ed";
        }

        public string GetCoinMarketCapApiUrl()
        {
            return "https://pro-api.coinmarketcap.com";
        }

        public string GetCoinMarketCapQuoteApiUrl()
        {
            return "v1/cryptocurrency/quotes/latest";
        }
    }
}
