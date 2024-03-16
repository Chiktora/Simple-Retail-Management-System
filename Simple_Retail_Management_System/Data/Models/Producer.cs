using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Retail_Management_System.Data.Models
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property
        public virtual ICollection<Product> Products { get; set; }
    }
}
