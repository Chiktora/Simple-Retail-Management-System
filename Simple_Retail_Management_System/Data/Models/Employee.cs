namespace Simple_Retail_Management_System.Data.Models
{
    public class Employee
    {
        public Employee()
        {
            
        }
        public Employee(string name, string phoneNumber, string email, string position, DateTime hireDate)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Position = position;
            HireDate = hireDate;
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }

        public List<Sale> Sales { get; set; }
    }
}
