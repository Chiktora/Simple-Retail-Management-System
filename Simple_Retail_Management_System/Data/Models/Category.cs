using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Retail_Management_System.Data.Models
{
    public class Category
    {
        public Category()
        {
            
        }
        public Category(string categoryName)
        {
            CategoryName = categoryName;
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}
