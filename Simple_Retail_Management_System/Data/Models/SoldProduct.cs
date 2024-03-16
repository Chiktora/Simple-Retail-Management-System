using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Retail_Management_System.Data.Models
{ 
    public class SoldProduct
    {
        // Assuming SoldProduct has its own Id
        public int Id { get; set; }
        public int ReceiptHistoryId { get; set; }
        public int ProductId { get; set; }

        // Navigation properties
        public virtual ReceiptHistory ReceiptHistory { get; set; }
        public virtual Product Product { get; set; }
    }

}
