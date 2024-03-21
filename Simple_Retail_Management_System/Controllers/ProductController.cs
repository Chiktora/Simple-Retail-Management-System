using Simple_Retail_Management_System.Data.Models;
using Simple_Retail_Management_System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Simple_Retail_Management_System.Controllers
{
    public class ProductController
    {
         private readonly ShopContext context;

        public ProductController()
        {
            context = new ShopContext();
        }

        public void AddProduct(string barcode, string name, int stockQuantity, decimal price, string producerName, string categoryName, string additionalText)
        {
            var producer = context.Producers.FirstOrDefault(p => p.Name == producerName);
            if (producer == null)
            {
                throw new ArgumentException($"Producer with name {producerName} not found.");
            }

            var category = context.Categories.FirstOrDefault(c => c.CategoryName == categoryName);
            if (category == null)
            {
                throw new ArgumentException($"Category with name {categoryName} not found.");
            }

            var product = new Product
            {
                Barcode = barcode,
                Name = name,
                StockQuantity = stockQuantity,
                Price = price,
                ProducerId = producer.Id,
                CategoryId = category.Id,
                AdditionalText = additionalText
            };

            context.Products.Add(product);
            context.SaveChanges();
        }

        public void DeleteProduct(string barcode)
        {
            var product = context.Products.FirstOrDefault(p => p.Barcode == barcode);
            if (product == null)
            {
                throw new ArgumentException($"Product with barcode {barcode} not found.");
            }

            context.Products.Remove(product);
            context.SaveChanges();
        }

        public void EditProduct(string barcode, string newName, int newStockQuantity, decimal newPrice, string newProducerName, string newCategoryName, string newAdditionalText)
        {
            var product = context.Products
                .Include(p => p.Producer)
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Barcode == barcode);

            if (product == null)
            {
                throw new ArgumentException($"Product with barcode {barcode} not found.");
            }

            var producer = context.Producers.FirstOrDefault(p => p.Name == newProducerName);
            var category = context.Categories.FirstOrDefault(c => c.CategoryName == newCategoryName);

            if (producer != null)
            {
                product.ProducerId = producer.Id;
            }

            if (category != null)
            {
                product.CategoryId = category.Id;
            }

            product.Name = newName;
            product.StockQuantity = newStockQuantity;
            product.Price = newPrice;
            product.AdditionalText = newAdditionalText;

            context.SaveChanges();
        }

        public Product ReadProduct(string barcode)
        {
            var product = context.Products
                .Include(p => p.Producer)
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Barcode == barcode);

            if (product == null)
            {
                throw new ArgumentException($"Product with barcode {barcode} not found.");
            }

            return product;
        }
    }
}
