namespace Simple_Retail_Management_System.Data.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal SalesPrice { get; set; }

        public Employee Employee { get; set; }
        public Customer Customer { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}