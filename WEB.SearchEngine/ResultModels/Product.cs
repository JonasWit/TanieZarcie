using System;

namespace SearchEngine.SearchResultsModels
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Producer { get; set; }
        public string Seller { get; set; }
        public string Category { get; set; }
        public string SourceUrl { get; set; }
        public int ValueZl { get; set; }
        public int ValueGr { get; set; }
        public int Value { get; set; }
        public string Url { get; set; }
        public DateTime DownloadDate { get; set; }
    }
}
