using System;
using System.Collections.Generic;
using System.Text;

namespace WEB.Shop.Domain.Models
{
    public class ShopData
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<PromoSheetUrl> PromoSheets { get; set; }
    }
}
