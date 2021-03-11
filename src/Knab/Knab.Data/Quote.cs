using System;

namespace Knab.Data
{
    public class Quote
    {
        public CurrencyType Currency { get; set; }
        public decimal Price { get; set; }
        public DateTime LastUpdated { get; set; }

    }
}
