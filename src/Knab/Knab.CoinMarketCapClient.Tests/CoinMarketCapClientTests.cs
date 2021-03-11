using System.Linq;
using System.Net.Http;
using Knab.Data;
using Knab.Data.Provider;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Knab.CoinMarketCapClient.Tests
{
    public class CoinMarketCapClientTests
    {
        private CoinMarketCapClient Client;
        private string cryptoBTN = "BTN";
        
        [SetUp]
        public void Setup()
        {
            var loggingMock = new Mock<ILogger<CoinMarketCapClient>>();

            Client = new CoinMarketCapClient(new HttpClient(), new ServiceConfig(), loggingMock.Object);
        }

        [Test]
        [TestCase(CurrencyType.AUD)]
        [TestCase(CurrencyType.BRL)]
        [TestCase(CurrencyType.EUR)]
        [TestCase(CurrencyType.GBP)]
        [TestCase(CurrencyType.USD)]
        public void GetQuotes_ShouldReturnOneQuote_WhenRequestContainOneCurrency(CurrencyType currencyType)
        {
            var result = Client.GetQuotes(cryptoBTN, new []{currencyType}).Result;

            Assert.True(result.Single().Currency == currencyType);
        }

        [Test]
        public void GetQuotes_ShouldReturnAllFountResults_WhenRequestContainMoreThanOneCurrency()
        {
            var currency1 = CurrencyType.AUD;
            var currency2 = CurrencyType.BRL;

            var result = Client.GetQuotes(cryptoBTN, new[] { currency1, currency2 }).Result;

            Assert.True(result.Count() == 2);
            Assert.True(result.Count(i => i.Currency == currency1) == 1);
            Assert.True(result.Count(i => i.Currency == currency2) == 1);
        }

        [Test]
        public void GetQuotes_ShouldReturnEmptyResult_WhenCurrencyTypeIncorrect()
        {
            var result = Client.GetQuotes(cryptoBTN, new[] { (CurrencyType)0 }).Result;

            Assert.False(result.Any());
        }
    }
}