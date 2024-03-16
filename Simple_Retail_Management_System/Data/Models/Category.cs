using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Retail_Management_System.Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } // Changed the property name to avoid conflict with class name

        // Navigation property
        public virtual ICollection<Product> Products { get; set; }
    }
}
