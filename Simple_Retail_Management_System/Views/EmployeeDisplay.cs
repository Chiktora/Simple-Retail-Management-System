namespace Simple_Retail_Management_System.Views
{
    public class EmployeeDisplay
{
    EmployeeController employeeController;

    public EmployeeDisplay()
    {
        employeeController = new EmployeeController();
        EmployeeInput();
    }

    public void EmployeeMenu()
    {
        Console.WriteLine(new string('-', 14) + "EmployeeMenu" + new string('-', 14));
        Console.WriteLine("1. Add a new Employee");
        Console.WriteLine("2. Delete an Employee");
        Console.WriteLine("3. Find Employee by Id");
        Console.WriteLine("4. Show all Employees");
        Console.WriteLine("5. Update an Employee");
        Console.WriteLine(new string('-', 40));
    }


    private void EmployeeInput()
    {
        EmployeeMenu();

        int operation = int.Parse(Console.ReadLine());
        switch (operation)
        {
            case 1:
                AddEmployee();
                Console.WriteLine("Employee added.");
                break;
            case 2:
                DeleteEmployee();
                break;
            case 3:
                ShowEmployee();
                break;
            case 4:
                ShowAllEmployees();
                break;
            case 5:
                UpdateEmployee();
                break;
            default:
                Console.WriteLine("Option not available!\nReturning to Main Menu.");
                break;
        }

    }

    private void AddEmployee()
    {
        Employee emp = new Employee();

        Console.WriteLine($"Name: ");
        emp.Name = Console.ReadLine();
        Console.WriteLine($"Phone Number: ");
        emp.PhoneNumber = Console.ReadLine();
        Console.WriteLine($"Email: ");
        emp.Email = Console.ReadLine();
        Console.WriteLine($"Position: ");
        emp.Position = Console.ReadLine();

        employeeController.Add(emp);
    }


    private void DeleteEmployee()
    {
        Console.WriteLine("Enter ID to delete: ");
        int id = int.Parse(Console.ReadLine());
        Employee emp = employeeController.Get(id);
        if (emp != null)
        {
            employeeController.Delete(id);
            Console.WriteLine("Employee deleted.");
        }
        else
        {
            Console.WriteLine("Employee not found.");
        }
    }


    private void ShowEmployee()
    {
        Console.WriteLine("Enter ID to show: ");
        int id = int.Parse(Console.ReadLine());
        Employee emp = employeeController.Get(id);
        if (emp != null)
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("ID: " + emp.Id);
            Console.WriteLine("Name: " + emp.Name);
            Console.WriteLine("Phone Number: " + emp.PhoneNumber);
            Console.WriteLine("Email: " + emp.Email);
            Console.WriteLine("Position: " + emp.Position);
            Console.WriteLine("Hire Date: " + emp.HireDate);
        }
        else
        {
            Console.WriteLine("Employee not found.");
        }
    }


    private void ShowAllEmployees()
    {
        Console.WriteLine("All Employees:");
        var emps = employeeController.GetAll();
        foreach (var emp in emps)
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("ID: " + emp.Id);
            Console.WriteLine("Name: " + emp.Name);
            Console.WriteLine("Phone Number: " + emp.PhoneNumber);
            Console.WriteLine("Email: " + emp.Email);
            Console.WriteLine("Position: " + emp.Position);
            Console.WriteLine("Hire Date: " + emp.HireDate);
            Console.WriteLine(new string('-', 40));
        }
    }


    private void UpdateEmployee()
    {
        Console.WriteLine("Enter ID to Update: ");
        int id = int.Parse(Console.ReadLine());
        Employee emp = employeeController.Get(id);
        if (emp != null)
        {
            Console.WriteLine($"Name: ");
            emp.Name = Console.ReadLine();
            Console.WriteLine($"Phone Number: ");
            emp.PhoneNumber = Console.ReadLine();
            Console.WriteLine($"Email: ");
            emp.Email = Console.ReadLine();
            Console.WriteLine($"Position: ");
            emp.Position = Console.ReadLine();

            employeeController.Update(emp);
        }
        else
        {
            Console.WriteLine("Employee not found!");
        }
    }



}
}
