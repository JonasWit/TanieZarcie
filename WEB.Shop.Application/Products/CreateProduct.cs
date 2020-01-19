using System;
using System.Collections.Generic;
using System.Text;
using WEB.Shop.DataBase;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.Products
{
    public class CreateProduct
    {
        private ApplicationDbContext _context;

        public CreateProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Do(int id, string name, string description)
        {
            _context.Products.Add(new Product { Id = id, Name = name, Description = description });


        }






    }
}
