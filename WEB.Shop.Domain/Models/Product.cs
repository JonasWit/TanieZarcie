using System;
using System.Collections.Generic;

namespace WEB.Shop.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int ProducerId { get; set; }
        public Producer Producer { get; set; }

        public int DistributorId { get; set; }
        public Distributor Distributor { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }


        public string SourceUrl { get; set; }
        public decimal Value { get; set; }

        public bool OnSale { get; set; }
        public decimal SaleValue { get; set; }
        public string SaleDescription { get; set; }
        public DateTime SaleDeadline { get; set; }

        public DateTime TimeStamp { get; set; }

        public ICollection<Stock> Stock { get; set; }
    }
}
