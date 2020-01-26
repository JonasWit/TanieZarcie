using System.Collections.Generic;
using SearchEngine.SearchResultsModels;

namespace SearchEngine
{
    public interface ISearchEngineCore
    {
        List<Product> GetAllData();
        List<Product> GetDemoData();
    }
}