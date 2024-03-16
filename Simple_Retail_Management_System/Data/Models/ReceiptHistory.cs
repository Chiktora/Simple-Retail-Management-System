using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Retail_Management_System.Data.Models
{
    public class ReceiptHistory
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public DateTime Timestamp { get; set; }
        public int PersonId { get; set; }
        public bool Is_Deleted { get; set; }

        // Navigation property
        public virtual Store Store { get; set; }
        public virtual ICollection<SoldProduct> SoldProducts { get; set; }
    }
}
