using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Retail_Management_System.Data.Models
{
    public class Producer
    {
        public Producer()
        {
            
        }
        public Producer(int name, int phoneNumber, int email)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public List<Product> Products { get; set; }
    }
}
