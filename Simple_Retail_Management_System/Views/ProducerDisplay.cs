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
            Console.Clear();
            Console.WriteLine("╔" + new string('═', 14) + " ProducerMenu " + new string('═', 14) + "╗");
            Console.WriteLine("║ 1. Add a new Producer                    ║");
            Console.WriteLine("║ 2. Delete a Producer                     ║");
            Console.WriteLine("║ 3. Find Producer by Id                   ║");
            Console.WriteLine("║ 4. Show all Producers                    ║");
            Console.WriteLine("║ 5. Update a Producer                     ║");
            Console.WriteLine("║ 6. Exit Producer Menu                    ║");
            Console.WriteLine("╚" + new string('═', 42) + "╝");

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
            Console.Clear();
            Console.WriteLine("╔" + new string('═', 40) + "╗");
            Console.WriteLine("║" + "      Add New Producer      ".PadRight(40) + "║");
            Console.WriteLine("╚" + new string('═', 40) + "╝");

            Producer prod = new Producer();

            Console.Write("Name: ");
            prod.Name = Console.ReadLine().Trim();
            Console.Write("Phone Number: ");
            prod.PhoneNumber = Console.ReadLine().Trim();
            Console.Write("Email: ");
            prod.Email = Console.ReadLine().Trim();

            producerController.Add(prod);
            Console.WriteLine("\nProducer added successfully.\nPress any key to continue...");
            Console.ReadKey();
        }



        private void DeleteProducer()
        {
            Console.Clear();
            Console.WriteLine("╔" + new string('═', 40) + "╗");
            Console.WriteLine("║" + "      Delete Producer      ".PadRight(40) + "║");
            Console.WriteLine("╚" + new string('═', 40) + "╝");

            Console.Write("Enter ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Producer prod = producerController.Get(id);
                if (prod != null)
                {
                    producerController.Delete(id);
                    Console.WriteLine("Producer deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Producer not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a numeric ID.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }



        private void ShowProducer()
        {
            Console.Clear();
            Console.WriteLine("╔" + new string('═', 40) + "╗");
            Console.WriteLine("║" + "      Show Producer Details      ".PadRight(40) + "║");
            Console.WriteLine("╚" + new string('═', 40) + "╝");

            Console.Write("Enter ID to show: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
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
            else
            {
                Console.WriteLine("Invalid input. Please enter a numeric ID.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }



        private void ShowAllProducers()
        {
            Console.Clear();
            Console.WriteLine("╔" + new string('═', 40) + "╗");
            Console.WriteLine("║" + "      All Producers      ".PadRight(40) + "║");
            Console.WriteLine("╚" + new string('═', 40) + "╝");

            var prods = producerController.GetAll();
            foreach (var prod in prods)
            {
                Console.WriteLine(new string('-', 40));
                Console.WriteLine("ID: " + prod.Id);
                Console.WriteLine("Name: " + prod.Name);
                Console.WriteLine("Phone Number: " + prod.PhoneNumber);
                Console.WriteLine("Email: " + prod.Email);
            }

            Console.WriteLine(new string('-', 40) + "\nPress any key to continue...");
            Console.ReadKey();
        }


        private void UpdateProducer()
        {
            Console.Clear();
            Console.WriteLine("╔" + new string('═', 40) + "╗");
            Console.WriteLine("║" + "      Update Producer      ".PadRight(40) + "║");
            Console.WriteLine("╚" + new string('═', 40) + "╝");

            Console.Write("Enter ID to Update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Producer prod = producerController.Get(id);
                if (prod != null)
                {
                    Console.Write("New Name: ");
                    prod.Name = Console.ReadLine().Trim();
                    Console.Write("New Phone Number: ");
                    prod.PhoneNumber = Console.ReadLine().Trim();
                    Console.Write("New Email: ");
                    prod.Email = Console.ReadLine().Trim();

                    producerController.Update(prod);
                    Console.WriteLine("\nProducer updated successfully.");
                }
                else
                {
                    Console.WriteLine("Producer not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a numeric ID.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

    }    
}
