﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB.Shop.DataBase;

namespace WEB.Shop.Application.OrdersAdmin
{
    public class UpdateOrder
    {
        private ApplicationDbContext _context;

        public UpdateOrder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DoAsync(int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);

            order.Status = order.Status + 1;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
