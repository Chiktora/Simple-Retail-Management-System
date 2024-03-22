using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Retail_Management_System.Data.Models
{
    public class Product
    {
        public Product()
        {
            
        }
        public Product(string barcode, string name, int stockQuantity, decimal price, int producerId, int categoryId, string additionalText)
        {
            Barcode = barcode;
            Name = name;
            StockQuantity = stockQuantity;
            Price = price;
            ProducerId = producerId;
            CategoryId = categoryId;
            AdditionalText = additionalText;
        }
        
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public int? ProducerId { get; set; }
        public int? CategoryId { get; set; }
        public string AdditionalText { get; set; }

        public Producer Producer { get; set; }
        public Category Category { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
