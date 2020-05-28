using System.Collections.Generic;

namespace WEB.Shop.Domain.Models
{
    public class Producer
    {
        public int Id { get; set; }
        public string ProducerName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
