using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Shop.Application.Products;

namespace WEB.Shop.UI.ViewModels.Summaries
{
    public class SummariesIndexViewModel
    {
        public List<GetProducts.ProductViewModel> Discounts { get; set; }
    }
}
