using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Retail_Management_System.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        // Foreign keys
        public int PriceId { get; set; }
        public int ProducerId { get; set; }
        public int CategoryId { get; set; }

        public string AdditionalText { get; set; }

        // Navigation properties
        public virtual Price Price { get; set; }
        public virtual Producer Producer { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<SoldProduct> SoldProducts { get; set; }
    }
}
