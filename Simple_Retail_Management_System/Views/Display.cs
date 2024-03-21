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
            do
            {
                Menu();
                int operation = int.Parse(Console.ReadLine());
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
                        OrderDetailDisplay orderDetailDisplay = new OrderDetailDisplay();
                        break;
                    case 5:
                        ProducerDisplay producerDisplay = new ProducerDisplay();
                        break;
                    case 6:
                        ProductDisplay productDisplay = new ProductDisplay();
                        break;
                    case 7:
                        SalesDisplay salesDisplay = new SalesDisplay();
                        break;
                    case 8: 
                        break;
                    default:
                        Console.WriteLine("Option is not available!");
                        break;
                }
            } while (operation != 8);
        }


        public void ShowMainMenu()
        {
            Console.WriteLine(new string('-', 18) + "MENU" + new string('-', 18));
            Console.WriteLine("1.Category Menu");
            Console.WriteLine("2.Customer Menu");
            Console.WriteLine("3.Employee Menu");
            Console.WriteLine("4.Order Details Menu");
            Console.WriteLine("5.Producer Menu");
            Console.WriteLine("6.Product Menu");
            Console.WriteLine("7.Sales Menu");
            Console.WriteLine(new string('-', 40));
        }//utre she napravq samite klasove
    }
}
