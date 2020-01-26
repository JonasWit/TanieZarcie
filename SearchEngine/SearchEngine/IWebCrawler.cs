using SearchEngine.SearchResultsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine.SearchEngine
{
    public interface IWebCrawler
    {
        List<Product> Products { get; set; }

        void GetData();
    }
}
