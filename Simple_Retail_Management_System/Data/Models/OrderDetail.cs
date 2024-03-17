namespace Simple_Retail_Management_System.Data.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Sale Sale { get; set; }
        public Product Product { get; set; }
    }
}