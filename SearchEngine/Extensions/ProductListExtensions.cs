using SearchEngine.SearchResultsModels;
using System;
using System.Collections.Generic;
using System.Text;
using Korzh.EasyQuery.Linq;
using System.Linq;

namespace SearchEngine.Extensions
{
    public static class ProductListExtensions
    {
        public static List<Product> SearchByString(this List<Product> inputList, string stringToken)
        {
            if (!string.IsNullOrEmpty(stringToken))
            {
                var products = inputList.AsQueryable();
                IQueryable<Product> searchResult = products.FullTextSearchQuery(stringToken);
                return searchResult.ToList();
            }
            else
            {
                return inputList;
            }
        }
    }
}
