

using Simple_Retail_Management_System.Controllers;
using Simple_Retail_Management_System.Data.Models;

public class CustomerDisplay
{
    CustomerController customerController;

    public CustomerDisplay()
    {
        customerController = new CustomerController();
        CustomerInput();
    }

    public void CustomerMenu()
    {
        Console.WriteLine(new string('-', 14) + "CustomerMenu" + new string('-', 14));
        Console.WriteLine("1. Add a new Customer");
        Console.WriteLine("2. Delete a Customer");
        Console.WriteLine("3. Find Customer by Id");
        Console.WriteLine("4. Show all Customers");
        Console.WriteLine("5. Update a Customer");
        Console.WriteLine(new string('-', 40));
    }


    private void CustomerInput()
    {
        CustomerMenu();

        int operation = int.Parse(Console.ReadLine());
        switch (operation)
        {
            case 1:
                AddCustomer();
                Console.WriteLine("Customer added.");
                break;
            case 2:
                DeleteCustomer();
                break;
            case 3:
                ShowCustomer();
                break;
            case 4:
                ShowAllCustomers();
                break;
            case 5:
                UpdateCustomer();
                break;
            default:
                Console.WriteLine("Option not available!\nReturning to Main Menu.");
                break;
        }

    }

    private void AddCustomer()
    {
        Customer cus = new Customer();

        Console.WriteLine($"Name: ");
        cus.Name = Console.ReadLine();
        Console.WriteLine($"Phone Number: ");
        cus.PhoneNumber = Console.ReadLine();
        Console.WriteLine($"Email: ");
        cus.Email = Console.ReadLine();

        customerController.Add(cus);
    }


    private void DeleteCustomer()
    {
        Console.WriteLine("Enter ID to delete: ");
        int id = int.Parse(Console.ReadLine());
        Customer cus = customerController.Get(id);
        if (cus != null)
        {
            customerController.Delete(id);
            Console.WriteLine("Customer deleted.");
        }
        else
        {
            Console.WriteLine("Customer not found.");
        }
    }


    private void ShowCustomer()
    {
        Console.WriteLine("Enter ID to show: ");
        int id = int.Parse(Console.ReadLine());
        Customer cus = customerController.Get(id);
        if (cus != null)
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("ID: " + cus.Id);
            Console.WriteLine("Name: " + cus.Name);
            Console.WriteLine("Phone Number: " + cus.PhoneNumber);
            Console.WriteLine("Email: " + cus.Email);
        }
        else
        {
            Console.WriteLine("Customer not found.");
        }
    }


    private void ShowAllCustomers()
    {
        Console.WriteLine("All Customers:");
        var custom = customerController.GetAll();
        foreach (var cus in custom)
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("ID: " + cus.Id);
            Console.WriteLine("Name: " + cus.Name);
            Console.WriteLine("Phone Number: " + cus.PhoneNumber);
            Console.WriteLine("Email: " + cus.Email);
            Console.WriteLine(new string('-', 40));
        }
    }


    private void UpdateCustomer()
    {
        Console.WriteLine("Enter ID to Update: ");
        int id = int.Parse(Console.ReadLine());
        Customer cus = customerController.Get(id);
        if (cus != null)
        {
            Console.WriteLine($"Name: ");
            cus.Name = Console.ReadLine();
            Console.WriteLine($"Phone Number: ");
            cus.PhoneNumber = Console.ReadLine();
            Console.WriteLine($"Email: ");
            cus.Email = Console.ReadLine();

            customerController.Update(cus);
        }
        else
        {
            Console.WriteLine("Customer not found!");
        }
    }



}
    

