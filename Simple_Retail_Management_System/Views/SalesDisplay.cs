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
            Console.WriteLine(new string('-', 14) + "Sales Menu" + new string('-', 14));
            Console.WriteLine("1. Process a new Sale");
            Console.WriteLine("2. Delete a Sale");
            Console.WriteLine("3. Read Sale Details");
            Console.WriteLine("4. Exit Sales ");
            Console.WriteLine(new string('-', 40));
        }

        public static void ProcessSale(SalesController salesService)
        {
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
            Console.WriteLine("Enter sale ID to retrieve its details:");
            int saleId = Convert.ToInt32(Console.ReadLine());

            try
            {
                var saleDetails = salesService.GetSaleDetails(saleId);

                Console.WriteLine($"Sale ID: {saleDetails.SaleId}");
                Console.WriteLine($"Employee Name: {saleDetails.EmployeeName}");
                Console.WriteLine($"Sale Date: {saleDetails.SaleDate}");
                Console.WriteLine("Sold Products and Quantities:");
                foreach (var product in saleDetails.SoldProducts)
                {
                    Console.WriteLine($"- {product.ProductName}, Quantity: {product.Quantity}");
                }
                Console.WriteLine($"Total Price: {saleDetails.TotalPrice:C}");

                if (saleDetails.Customer != null)
                {
                    Console.WriteLine("Customer Details:");
                    Console.WriteLine($"Name: {saleDetails.Customer.Name}");
                    Console.WriteLine($"Phone: {saleDetails.Customer.PhoneNumber}");
                    Console.WriteLine($"Email: {saleDetails.Customer.Email}");
                }
                else
                {
                    Console.WriteLine("No customer details available for this sale.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public static void DeleteSale(SalesController salesService)
        {
            Console.WriteLine("Enter the ID of the sale you wish to delete:");
            int saleId;
            while (!int.TryParse(Console.ReadLine(), out saleId))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer for the sale ID:");
            }

            try
            {
                salesService.DeleteSale(saleId);
                Console.WriteLine($"Sale with ID {saleId} has been successfully deleted.");
            }
            catch (ArgumentException ex)
            {
                // This catch block is specifically for handling cases where the sale ID does not exist
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                // General exception handling for other unforeseen errors
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

    }
}