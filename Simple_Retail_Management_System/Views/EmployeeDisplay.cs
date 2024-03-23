using Simple_Retail_Management_System.Controllers;
using Simple_Retail_Management_System.Data.Models;

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
        /// <summary>
        /// Prints out a menu for employee
        /// </summary>
        public void EmployeeMenu()
        {
            Console.Clear();
            Console.WriteLine("╔" + new string('═', 14) + " EmployeeMenu " + new string('═', 14) + "╗");
            Console.WriteLine("║ 1. Add a new Employee                    ║");
            Console.WriteLine("║ 2. Delete an Employee                    ║");
            Console.WriteLine("║ 3. Find Employee by Id                   ║");
            Console.WriteLine("║ 4. Show all Employees                    ║");
            Console.WriteLine("║ 5. Update an Employee                    ║");
            Console.WriteLine("║ 6. Exit Employee Menu                    ║");
            Console.WriteLine("╚" + new string('═', 42) + "╝");

        }

        /// <summary>
        /// Enter a number based on the menu and it executes the corresponding method
        /// </summary>
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
        /// <summary>
        /// Adds a new employee based on the inputs of the user
        /// </summary>
        private void AddEmployee()
        {
            Console.Clear();
            DisplayHeader("Add New Employee");

            Employee emp = new Employee();

            Console.Write("Name: ");
            emp.Name = Console.ReadLine().Trim();
            Console.Write("Phone Number: ");
            emp.PhoneNumber = Console.ReadLine().Trim();
            Console.Write("Email: ");
            emp.Email = Console.ReadLine().Trim();
            Console.Write("Position: ");
            emp.Position = Console.ReadLine().Trim();

            employeeController.Add(emp);
            Console.WriteLine("\nEmployee added successfully.");

            PromptContinue();
        }


        /// <summary>
        /// Deletes a employee by the ID given
        /// </summary>
        private void DeleteEmployee()
        {
            Console.Clear();
            DisplayHeader("Delete Employee");

            Console.Write("Enter ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Employee emp = employeeController.Get(id);
                if (emp != null)
                {
                    employeeController.Delete(id);
                    Console.WriteLine("\nEmployee deleted.");
                }
                else
                {
                    Console.WriteLine("\nEmployee not found.");
                }
            }
            else
            {
                Console.WriteLine("\nInvalid input. Please enter a numeric ID.");
            }

            PromptContinue();
        }

        /// <summary>
        /// Prints out the properties of a employee by ID
        /// </summary>
        private void ShowEmployee()
        {
            Console.Clear();
            DisplayHeader("Show Employee Details");

            Console.Write("Enter ID to show: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Employee emp = employeeController.Get(id);
                if (emp != null)
                {
                    DisplayEmployeeDetails(emp);
                }
                else
                {
                    Console.WriteLine("\nEmployee not found.");
                }
            }
            else
            {
                Console.WriteLine("\nInvalid input. Please enter a numeric ID.");
            }

            PromptContinue();
        }

        /// <summary>
        /// Prints out the properties of all employees
        /// </summary>
        private void ShowAllEmployees()
        {
            Console.Clear();
            DisplayHeader("All Employees");

            var emps = employeeController.GetAll();
            foreach (var emp in emps)
            {
                DisplayEmployeeDetails(emp);
            }

            PromptContinue();
        }

        /// <summary>
        /// Updates the properties of a employee by ID
        /// </summary>
        private void UpdateEmployee()
        {
            Console.Clear();
            DisplayHeader("Update Employee");

            Console.Write("Enter ID to Update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Employee emp = employeeController.Get(id);
                if (emp != null)
                {
                    Console.Write("New Name: ");
                    emp.Name = Console.ReadLine().Trim();
                    Console.Write("New Phone Number: ");
                    emp.PhoneNumber = Console.ReadLine().Trim();
                    Console.Write("New Email: ");
                    emp.Email = Console.ReadLine().Trim();
                    Console.Write("New Position: ");
                    emp.Position = Console.ReadLine().Trim();

                    employeeController.Update(emp);
                    Console.WriteLine("\nEmployee updated successfully.");
                }
                else
                {
                    Console.WriteLine("\nEmployee not found.");
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

        private void DisplayEmployeeDetails(Employee emp)
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine($"ID: {emp.Id}");
            Console.WriteLine($"Name: {emp.Name}");
            Console.WriteLine($"Phone Number: {emp.PhoneNumber}");
            Console.WriteLine($"Email: {emp.Email}");
            Console.WriteLine($"Position: {emp.Position}");
            Console.WriteLine($"Hire Date: {emp.HireDate.ToString("d")}");
            Console.WriteLine(new string('-', 40));
        }

        private void PromptContinue()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }




    }
}
