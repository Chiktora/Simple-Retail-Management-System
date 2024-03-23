using Simple_Retail_Management_System.Controllers;

namespace Simple_Retail_Management_System.Views
{
    public class SalesDisplay
    {
        SalesController salesController;
        public SalesDisplay() 
        {
            salesController = new SalesController();
            SalesInput();
        }

        private void SalesInput()
        {
            int operation = 0;
            do
            {
                SalesMenu();
                if (!int.TryParse(Console.ReadLine(), out operation))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                switch (operation)
                {
                    case 1:
                        ProcessSale(salesController);
                        break;
                    case 2:
                        DeleteSale(salesController);
                        break;
                    case 3:
                        ReadSale(salesController);
                        break;
                    default:
                        Console.WriteLine("Option not available!\nReturning to Main Menu.");
                        break;
                }
            } while (operation != 4); 
        }

        private void SalesMenu()
        {
            Console.Clear();
            Console.WriteLine("╔" + new string('═', 15) + " Sales Menu " + new string('═', 15) + "╗");
            Console.WriteLine("║ 1. Process a new Sale                    ║");
            Console.WriteLine("║ 2. Delete a Sale                         ║");
            Console.WriteLine("║ 3. Read Sale Details                     ║");
            Console.WriteLine("║ 4. Exit Sales                            ║");
            Console.WriteLine("╚" + new string('═', 42) + "╝");

        }

        public static void ProcessSale(SalesController salesService)
        {
            Console.Clear(); // Clears the console for a clean start
            Console.WriteLine("╔" + new string('═', 48) + "╗");
            Console.WriteLine("║" + "              Process a New Sale".PadRight(48) + "║");
            Console.WriteLine("╚" + new string('═', 48) + "╝");
            Console.WriteLine("Enter employee ID:");
            int employeeId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter customer ID:");
            int customerId = Convert.ToInt32(Console.ReadLine());

            var productBarcodesAndQuantities = new Dictionary<string, int>();
            while (true)
            {
                Console.WriteLine("Enter product barcode (or type 'done' to finish):");
                string barcode = Console.ReadLine();
                if (barcode.Equals("done", StringComparison.OrdinalIgnoreCase)) break;

                Console.WriteLine($"Enter quantity for product with barcode {barcode}:");
                int quantity = Convert.ToInt32(Console.ReadLine());

                productBarcodesAndQuantities.Add(barcode, quantity);
            }

            // This date could also be the current date or retrieved from elsewhere
            DateTime saleDate = DateTime.Now;

            try
            {
                // Process the sale
                salesService.AddSaleWithProductQuantities(employeeId, customerId, saleDate , productBarcodesAndQuantities);
                Console.WriteLine("Sale processed successfully.");
            }
            catch (ArgumentException ex)
            {
                // Handle any errors that occur during the sale process
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public static void ReadSale(SalesController salesService)
        {
            Console.Clear(); // Clears the console for a clean start
            Console.WriteLine("╔" + new string('═', 58) + "╗");
            Console.WriteLine("║" + "                  Retrieve Sale Details                  ".PadRight(58) + "║");
            Console.WriteLine("╚" + new string('═', 58) + "╝");
            Console.WriteLine("\nPlease enter the sale ID to retrieve its details:\n");

            int saleId;
            Console.Write("Sale ID: ");
            while (!int.TryParse(Console.ReadLine(), out saleId))
            {
                Console.WriteLine("\nInvalid input. Please enter a valid integer for the sale ID.");
                Console.Write("Sale ID: ");
            }

            try
            {
                var saleDetails = salesService.GetSaleDetails(saleId);
                Console.WriteLine("\nSale Details:");
                Console.WriteLine(new string('-', 30));
                Console.WriteLine($"Sale ID: {saleDetails.SaleId}");
                Console.WriteLine($"Employee Name: {saleDetails.EmployeeName}");
                Console.WriteLine($"Sale Date: {saleDetails.SaleDate.ToShortDateString()}");

                Console.WriteLine("\nSold Products and Quantities:");
                foreach (var product in saleDetails.SoldProducts)
                {
                    Console.WriteLine($"- {product.ProductName}, Quantity: {product.Quantity}");
                }
                Console.WriteLine(new string('-', 30));
                Console.WriteLine($"Total Price: {saleDetails.TotalPrice:C}");

                if (saleDetails.Customer != null)
                {
                    Console.WriteLine("\nCustomer Details:");
                    Console.WriteLine($"Name: {saleDetails.Customer.Name}");
                    Console.WriteLine($"Phone: {saleDetails.Customer.PhoneNumber}");
                    Console.WriteLine($"Email: {saleDetails.Customer.Email}");
                }
                else
                {
                    Console.WriteLine("\nNo customer details available for this sale.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }
       
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }


        public static void DeleteSale(SalesController salesService)
        {
            Console.Clear(); // Optional: Clears the console for a clean start
            Console.WriteLine("╔" + new string('═', 58) + "╗");
            Console.WriteLine("║" + "                   Delete a Sale                   ".PadRight(58) + "║");
            Console.WriteLine("╚" + new string('═', 58) + "╝");
            Console.WriteLine("\nPlease enter the ID of the sale you wish to delete:\n");

            int saleId;
            Console.Write("Sale ID: ");
            while (!int.TryParse(Console.ReadLine(), out saleId))
            {
                Console.WriteLine("\nInvalid input. Please enter a valid integer for the sale ID.");
                Console.Write("Sale ID: ");
            }

            try
            {
                salesService.DeleteSale(saleId);
                Console.WriteLine($"\nSale with ID {saleId} has been successfully deleted.");
            }
            catch (ArgumentException ex)
            {
                // This catch block is specifically for handling cases where the sale ID does not exist
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                // General exception handling for other unforeseen errors
                Console.WriteLine($"\nAn unexpected error occurred: {ex.Message}");
            }

            // Optional: Add a pause here to let the user read the message before returning to the main menu
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

    }
}