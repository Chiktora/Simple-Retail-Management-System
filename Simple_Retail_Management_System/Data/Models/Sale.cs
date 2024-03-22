namespace Simple_Retail_Management_System.Data.Models
{
    public class Sale
    {
        public Sale()
        {
            
        }
        public Sale(int employeeId, int customerId, DateTime saleDate, decimal salesPrice)//vse oshte ne znam dali dateTime sushtestvuva
        {
            EmployeeId = employeeId;
            CustomerId = customerId;
            SaleDate = saleDate;
            SalesPrice = salesPrice;
        }
        
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
