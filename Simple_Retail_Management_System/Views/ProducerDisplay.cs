using Simple_Retail_Management_System.Controllers;
using Simple_Retail_Management_System.Data.Models;

namespace Simple_Retail_Management_System.Views
{
    public class ProducerDisplay
{
    ProducerController producerController;

        public ProducerDisplay()
        {
            producerController = new ProducerController();
            ProducerInput();
        }

        public void ProducerMenu()
        {
            Console.WriteLine(new string('-', 14) + "ProducerMenu" + new string('-', 14));
            Console.WriteLine("1. Add a new Producer");
            Console.WriteLine("2. Delete a Producer");
            Console.WriteLine("3. Find Producer by Id");
            Console.WriteLine("4. Show all Producers");
            Console.WriteLine("5. Update a Producer");
            Console.WriteLine(new string('-', 40));
        }


        private void ProducerInput()
        {
            ProducerMenu();

            int operation = int.Parse(Console.ReadLine());
            switch (operation)
            {
                case 1:
                    AddProducer();
                    Console.WriteLine("Producer added.");
                    break;
                case 2:
                    DeleteProducer();
                    break;
                case 3:
                    ShowProducer();
                    break;
                case 4:
                    ShowAllProducers();
                    break;
                case 5:
                    UpdateProducer();
                    break;
                default:
                    Console.WriteLine("Option not available!\nReturning to Main Menu.");
                    break;
            }

        }

        private void AddProducer()
        {
            Producer prod = new Producer();

            Console.WriteLine($"Name: ");
            prod.Name = Console.ReadLine();
            Console.WriteLine($"Phone Number: ");
            prod.PhoneNumber = Console.ReadLine();
            Console.WriteLine($"Email: ");
            prod.Email = Console.ReadLine();

            producerController.Add(prod);
        }


        private void DeleteProducer()
        {
            Console.WriteLine("Enter ID to delete: ");
            int id = int.Parse(Console.ReadLine());
            Producer prod = producerController.Get(id);
            if (prod != null)
            {
                producerController.Delete(id);
                Console.WriteLine("Producer deleted.");
            }
            else
            {
                Console.WriteLine("Producer not found.");
            }
        }


        private void ShowProducer()
        {
            Console.WriteLine("Enter ID to show: ");
            int id = int.Parse(Console.ReadLine());
            Producer prod = producerController.Get(id);
            if (prod != null)
            {
                Console.WriteLine(new string('-', 40));
                Console.WriteLine("ID: " + prod.Id);
                Console.WriteLine("Name: " + prod.Name);
                Console.WriteLine("Phone Number: " + prod.PhoneNumber);
                Console.WriteLine("Email: " + prod.Email);
            }
            else
            {
                Console.WriteLine("Producer not found.");
            }
        }


        private void ShowAllProducers()
        {
            Console.WriteLine("All Producers:");
            var prods = producerController.GetAll();
            foreach (var prod in prods)
            {
                Console.WriteLine(new string('-', 40));
                Console.WriteLine("ID: " + prod.Id);
                Console.WriteLine("Name: " + prod.Name);
                Console.WriteLine("Phone Number: " + prod.PhoneNumber);
                Console.WriteLine("Email: " + prod.Email);
                Console.WriteLine(new string('-', 40));
            }
        }


        private void UpdateProducer()
        {
            Console.WriteLine("Enter ID to Update: ");
            int id = int.Parse(Console.ReadLine());
            Producer prod = producerController.Get(id);
            if (prod != null)
            {
                Console.WriteLine($"Name: ");
            prod.Name = Console.ReadLine();
            Console.WriteLine($"Phone Number: ");
            prod.PhoneNumber = Console.ReadLine();
            Console.WriteLine($"Email: ");
            prod.Email = Console.ReadLine();

                producerController.Update(prod);
            }
            else
            {
                Console.WriteLine("Producer not found!");
            }
        }



    }
}
