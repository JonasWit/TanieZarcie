using System;
using System.Collections.Generic;
using System.Text;

namespace WEB.Shop.Application.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string ValueDispaly => $"{Value.ToString("N2")} pln";
    }
}
