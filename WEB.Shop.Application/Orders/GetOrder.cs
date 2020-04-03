using System;
using System.Collections.Generic;
using System.Linq;
using WEB.Shop.Domain.Extensions;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.Orders
{
    [TransientService]
    public class GetOrder
    {
        private IOrderManager _orderManager;

        public GetOrder(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public class Response
        {
            public string OrderReference { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }

            public IEnumerable<Product> Products { get; set; }

            public string TotalValue { get; set; }
        }

        public class Product
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public int Quantity { get; set; }
            public string StockDescription { get; set; }
        }

        public Response Do(int id) => _orderManager.GetOrderById(id, Projection);

        private static Func<Order, Response> Projection = (order) =>
            new Response
            {
                OrderReference = order.OrderReference,

                FirstName = order.FirstName,
                LastName = order.LastName,
                Email = order.Email,
                PhoneNumber = order.PhoneNumber,
                Address1 = order.Address1,
                Address2 = order.Address2,
                City = order.City,
                PostCode = order.PostCode,

                Products = order.OrderStocks.Select(orderStock => new Product
                {
                    Name = orderStock.Stock.Product.Name,
                    Description = orderStock.Stock.Product.Description,
                    Value = orderStock.Stock.Product.Value.MonetaryValue(),
                    Quantity = orderStock.Quantity,
                    StockDescription = orderStock.Stock.Description
                }),
                TotalValue = order.OrderStocks.Sum(orderStock => orderStock.Stock.Product.Value).MonetaryValue()
            };
    }
}
