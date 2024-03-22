namespace Simple_Retail_Management_System.Data.Models
{
    public class Customer
    {
        public Category()
        {
            
        }
        public Category(string name, string phoneNumber, string email)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public List<Sale> Sales { get; set; }
    }
}
