

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
        Console.Clear();
        Console.WriteLine("╔" + new string('═', 14) + " CustomerMenu " + new string('═', 14) + "╗");
        Console.WriteLine("║ 1. Add a new Customer                    ║");
        Console.WriteLine("║ 2. Delete a Customer                     ║");
        Console.WriteLine("║ 3. Find Customer by Id                   ║");
        Console.WriteLine("║ 4. Show all Customers                    ║");
        Console.WriteLine("║ 5. Update a Customer                     ║");
        Console.WriteLine("║ 6. Exit Customer Menu                    ║");
        Console.WriteLine("╚" + new string('═', 42) + "╝");

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
        Console.Clear();
        DisplayHeader("Add New Customer");

        Customer cus = new Customer();

        Console.Write("Name: ");
        cus.Name = Console.ReadLine().Trim();
        Console.Write("Phone Number: ");
        cus.PhoneNumber = Console.ReadLine().Trim();
        Console.Write("Email: ");
        cus.Email = Console.ReadLine().Trim();

        customerController.Add(cus);
        Console.WriteLine("\nCustomer added successfully.");

        PromptContinue();
    }



    private void DeleteCustomer()
    {
        Console.Clear();
        DisplayHeader("Delete Customer");

        Console.Write("Enter ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Customer cus = customerController.Get(id);
            if (cus != null)
            {
                customerController.Delete(id);
                Console.WriteLine("\nCustomer deleted.");
            }
            else
            {
                Console.WriteLine("\nCustomer not found.");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid input. Please enter a numeric ID.");
        }

        PromptContinue();
    }



    private void ShowCustomer()
    {
        Console.Clear();
        DisplayHeader("Show Customer Details");

        Console.Write("Enter ID to show: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Customer cus = customerController.Get(id);
            if (cus != null)
            {
                DisplayCustomerDetails(cus);
            }
            else
            {
                Console.WriteLine("\nCustomer not found.");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid input. Please enter a numeric ID.");
        }

        PromptContinue();
    }



    private void ShowAllCustomers()
    {
        Console.Clear();
        DisplayHeader("All Customers");

        var custom = customerController.GetAll();
        foreach (var cus in custom)
        {
            DisplayCustomerDetails(cus);
        }

        PromptContinue();
    }



    private void UpdateCustomer()
    {
        Console.Clear();
        DisplayHeader("Update Customer");

        Console.Write("Enter ID to Update: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Customer cus = customerController.Get(id);
            if (cus != null)
            {
                Console.Write("New Name: ");
                cus.Name = Console.ReadLine().Trim();
                Console.Write("New Phone Number: ");
                cus.PhoneNumber = Console.ReadLine().Trim();
                Console.Write("New Email: ");
                cus.Email = Console.ReadLine().Trim();

                customerController.Update(cus);
                Console.WriteLine("\nCustomer updated successfully.");
            }
            else
            {
                Console.WriteLine("\nCustomer not found.");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid input. Please enter a numeric ID.");
        }

        PromptContinue();
    }
    
    private void DisplayHeader(string title)
    {
        Console.WriteLine("╔" + new string('═', 50) + "╗");
        Console.WriteLine("║" + title.PadRight(50) + "║");
        Console.WriteLine("╚" + new string('═', 50) + "╝");
    }

    private void DisplayCustomerDetails(Customer cus)
    {
        Console.WriteLine(new string('-', 40));
        Console.WriteLine($"ID: {cus.Id}");
        Console.WriteLine($"Name: {cus.Name}");
        Console.WriteLine($"Phone Number: {cus.PhoneNumber}");
        Console.WriteLine($"Email: {cus.Email}");
        Console.WriteLine(new string('-', 40));
    }

    private void PromptContinue()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

}




    

