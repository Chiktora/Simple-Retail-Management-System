using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Retail_Management_System.Views
{
    public class Display
    {
        public Display()
        {
            Input();
        }

        public void Input()
        {
            int operation = -1;
            do
            {
                
                Menu();
                 operation = int.Parse(Console.ReadLine());
                switch (operation)
                {
                    case 1:
                        CategoryDisplay categoryDisplay = new CategoryDisplay();
                        break;
                    case 2:
                        CustomerDisplay customerDisplay = new CustomerDisplay();
                        break;
                    case 3:
                        EmployeeDisplay employeeDisplay = new EmployeeDisplay();
                        break;
                    case 4:
                        ProducerDisplay producerDisplay = new ProducerDisplay();
                        break;
                    case 5:
                        ProductDisplay productDisplay = new ProductDisplay();
                        break;
                    case 6:
                        SalesDisplay salesDisplay = new SalesDisplay();
                        break;
                    case 7:
                        Console.WriteLine("Program Exit");
                        break;
                    default:
                        Console.WriteLine("Option is not available!");
                        break;
                }
            } while (operation != 7);
        }


        public void Menu()
        {
            Console.Clear();
            Console.WriteLine("╔" + new string('═', 18) + " MENU " + new string('═', 18) + "╗");
            Console.WriteLine("║ 1. Category Menu                         ║");
            Console.WriteLine("║ 2. Customer Menu                         ║");
            Console.WriteLine("║ 3. Employee Menu                         ║");
            Console.WriteLine("║ 4. Producer Menu                         ║");
            Console.WriteLine("║ 5. Product Menu                          ║");
            Console.WriteLine("║ 6. Sales Menu                            ║");
            Console.WriteLine("║ 7. Program Exit                          ║");
            Console.WriteLine("╚" + new string('═', 42) + "╝");
        }
    }
}
