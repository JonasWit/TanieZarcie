using System;
using System.Collections.Generic;
using System.Text;
using WEB.SearchEngine.Enums;
using WEB.SearchEngine.SearchResultsModels;

namespace WEB.SearchEngine.Interfaces
{
    public interface ICrawler
    {
        List<Product> Products { get; set; }
        string BaseUrl { get; set; }
        string BaseUrlForProducts { get; set; }
        Shops Shop { get; set; }
    }
}
