using System;
using System.Collections.Generic;
using WEB.SearchEngine.ResultModels;

namespace WEB.SearchEngine.SearchResultsModels
{
    public class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Producer { get; set; }
        public string Seller { get; set; }
        public string Category { get; set; }
        public string SourceUrl { get; set; }
        public decimal Value { get; set; }
        public DateTime TimeStamp { get; set; }

        public ICollection<Stock> Stock { get; set; }
    }
}
