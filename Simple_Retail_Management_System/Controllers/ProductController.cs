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
         private ShopContext context;

        public ProductController()
        {
            context = new ShopContext();
        }
        public ProductController(ShopContext context)
        {
            context = new ShopContext();
        }
        /// <summary>
        /// Adds a new product to the context
        /// </summary>
        public void AddProduct(string barcode, string name, int stockQuantity, decimal price, string producerName, string categoryName, string additionalText)
        {
            //Check if the producer is present.
            var producer = context.Producers.FirstOrDefault(p => p.Name == producerName);
            if (producer == null)
            {
                throw new ArgumentException($"Producer with name {producerName} not found.");
            }

            //Check if the category is present.
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
        /// <summary>
        /// Deletes a product from the context by ID
        /// </summary>
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
        /// <summary>
        /// Updates an existing product
        /// </summary>
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

            //Check if the producer is present.
            if (producer != null)
            {
                product.ProducerId = producer.Id;
            }

            //Check if the category is present.
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
        /// <summary>
        /// Retrieves a product by ID
        /// </summary>
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
        /// <summary>
        /// Retrieves a list of products by the name of their producer.
        /// </summary>
        public List<Product> GetProductsByProducer(string producerName)
        {
            var products = context.Products
                .Include(p => p.Producer)
                .Where(p => p.Producer.Name == producerName)
                .ToList();

            return products;
        }
        /// <summary>
        /// Retrieves a list of products by their category name.
        /// </summary>
        public List<Product> GetProductsByCategory(string categoryName)
        {
            var products = context.Products
                .Include(p => p.Category)
                .Where(p => p.Category.CategoryName == categoryName)
                .ToList();

            return products;
        }
    }
}
