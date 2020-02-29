using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB.Shop.Application.Orders;
using WEB.Shop.DataBase;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.UI.Pages
{
    public class OrderModel : PageModel
    {
        private IOrderManager _orderManager;

        public OrderModel(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public GetOrder.Response Order { get; set; }

        public void OnGet(int id)
        {
            Order = new GetOrder(_orderManager).Do(id);
        }
    }
}
