using System;
using System.Collections.Generic;
using System.Text;

namespace WEB.Shop.Domain.Models
{
    public class Producer
    {
        public int Id { get; set; }
        public string ProducerName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
