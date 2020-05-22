using System;
using System.Collections.Generic;
using System.Text;

namespace WEB.Shop.Domain.Models
{
    public class Distributor
    {
        public int Id { get; set; }
        public string DistributorName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
