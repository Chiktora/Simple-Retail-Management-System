using Simple_Retail_Management_System.Controllers;
using Simple_Retail_Management_System.Data.Models;

namespace Simple_Retail_Management_System.Views
{
public class ProductDisplay
{
    ProductController productController;

    public ProductDisplay()
    {
        productController = new ProductController();
        ProductInput();
    }

    public void ProductMenu()
    {
            Console.Clear();
            Console.WriteLine("╔" + new string('═', 14) + " ProductsMenu " + new string('═', 14) + "╗");
            Console.WriteLine("║ 1. Add a new Product                     ║");
            Console.WriteLine("║ 2. Delete a Product                      ║");
            Console.WriteLine("║ 3. Find Product by barcode               ║");
            Console.WriteLine("║ 4. Update a Product                      ║");
            Console.WriteLine("║ 5. Show products by producer             ║");
            Console.WriteLine("║ 6. Show products by category             ║");
            Console.WriteLine("╚" + new string('═', 42) + "╝");

        }


        private void ProductInput()
    {
        ProductMenu();

        int operation = int.Parse(Console.ReadLine());
        switch (operation)
        {
            case 1:
                AddProduct();
                Console.WriteLine("Product added.");
                break;
            case 2:
                DeleteProduct();
                break;
            case 3:
                ShowProduct();
                break;
            case 4:
                UpdateProduct();
                break;
            case 5:
                ShowProductsByProducer();
                break;
            case 6:
                ShowProductsByCategory();
                break;
            default:
                Console.WriteLine("Option not available!\nReturning to Main Menu.");
                break;
        }

    }

        private void ShowProductsByCategory()
        {
            Console.Clear(); // Optional: Clears the console for a clean start
            Console.WriteLine("╔" + new string('═', 58) + "╗");
            Console.WriteLine("║" + "             Show Products by Category             ".PadRight(58) + "║");
            Console.WriteLine("╚" + new string('═', 58) + "╝");
            Console.WriteLine("\nEnter the name of the category to get products from:\n");

            Console.Write("Category Name: ");
            string categoryName = Console.ReadLine().Trim();

            List<Product> productsByCategory = productController.GetProductsByCategory(categoryName);

            if (productsByCategory.Any())
            {
                Console.WriteLine($"\nProducts in the '{categoryName}' category:");
                Console.WriteLine(new string('-', 30));
                foreach (var product in productsByCategory)
                {
                    Console.WriteLine($"- Name: {product.Name}, Barcode: {product.Barcode}, Price: {product.Price:C}");
                }
                Console.WriteLine(new string('-', 30));
            }
            else
            {
                Console.WriteLine($"\nNo products found in the '{categoryName}' category.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }


        private void ShowProductsByProducer()
        {
            Console.Clear(); // Optional: Clears the console for a clean start
            Console.WriteLine("╔" + new string('═', 58) + "╗");
            Console.WriteLine("║" + "             Show Products by Producer             ".PadRight(58) + "║");
            Console.WriteLine("╚" + new string('═', 58) + "╝");
            Console.WriteLine("\nEnter the name of the producer to get their products:\n");

            Console.Write("Producer Name: ");
            string producerName = Console.ReadLine().Trim();

            List<Product> productsByProducer = productController.GetProductsByProducer(producerName);

            if (productsByProducer.Any())
            {
                Console.WriteLine($"\nProducts by {producerName}:");
                Console.WriteLine(new string('-', 30));
                foreach (var product in productsByProducer)
                {
                    Console.WriteLine($"- Name: {product.Name}, Barcode: {product.Barcode}, Price: {product.Price:C}");
                }
                Console.WriteLine(new string('-', 30));
            }
            else
            {
                Console.WriteLine($"\nNo products found for producer {producerName}.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }


        private void AddProduct()
        {
            Console.Clear(); // Clears the console for a clean start
            Console.WriteLine("╔" + new string('═', 58) + "╗");
            Console.WriteLine("║" + "                Add a New Product                ".PadRight(58) + "║");
            Console.WriteLine("╚" + new string('═', 58) + "╝");
            Console.WriteLine("\nEnter product details below:");

            try
            {
                Console.Write("Barcode: ");
                string barcode = Console.ReadLine().Trim();
                Console.Write("Name: ");
                string name = Console.ReadLine().Trim();
                Console.Write("Stock Quantity: ");
                int stockQuantity;
                while (!int.TryParse(Console.ReadLine(), out stockQuantity) || stockQuantity < 0)
                {
                    Console.WriteLine("Invalid input. Please enter a non-negative integer for stock quantity.");
                    Console.Write("Stock Quantity: ");
                }
                Console.Write("Price: ");
                decimal price;
                while (!decimal.TryParse(Console.ReadLine(), out price) || price < 0)
                {
                    Console.WriteLine("Invalid input. Please enter a non-negative decimal for price.");
                    Console.Write("Price: ");
                }
                Console.Write("Producer Name: ");
                string producerName = Console.ReadLine().Trim();
                Console.Write("Category Name: ");
                string categoryName = Console.ReadLine().Trim();
                Console.Write("Additional Text: ");
                string additionalText = Console.ReadLine().Trim();

                productController.AddProduct(barcode, name, stockQuantity, price, producerName, categoryName, additionalText);
                Console.WriteLine("\nProduct added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void DeleteProduct()
        {
            Console.Clear(); // Clears the console for a clean start
            Console.WriteLine("╔" + new string('═', 58) + "╗");
            Console.WriteLine("║" + "              Delete a Product              ".PadRight(58) + "║");
            Console.WriteLine("╚" + new string('═', 58) + "╝");
            Console.WriteLine("\nEnter the barcode of the product to delete:");

            try
            {
                Console.Write("Barcode: ");
                string deleteBarcode = Console.ReadLine().Trim();
                productController.DeleteProduct(deleteBarcode);
                Console.WriteLine("\nProduct deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void ShowProduct()
        {
            Console.Clear(); // Clears the console for a clean start
            Console.WriteLine("╔" + new string('═', 58) + "╗");
            Console.WriteLine("║" + "              Show Product Details              ".PadRight(58) + "║");
            Console.WriteLine("╚" + new string('═', 58) + "╝");
            Console.WriteLine("\nEnter the barcode of the product to read:");

            try
            {
                Console.Write("Barcode: ");
                string readBarcode = Console.ReadLine().Trim();
                Product product = productController.ReadProduct(readBarcode);               
                 Console.WriteLine($"\nProduct Name: {product.Name},\nQuantity: {product.StockQuantity},\nPrice: {product.Price:C}");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }
            

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }


        private void UpdateProduct()
        {
            Console.Clear(); // Clears the console for a clean start
            Console.WriteLine("╔" + new string('═', 58) + "╗");
            Console.WriteLine("║" + "              Update Product Details              ".PadRight(58) + "║");
            Console.WriteLine("╚" + new string('═', 58) + "╝");
            Console.WriteLine("\nEnter product details to update:");

            try
            {
                Console.Write("Barcode: ");
                string editBarcode = Console.ReadLine().Trim();
                Console.Write("New Name: ");
                string newName = Console.ReadLine().Trim();
                Console.Write("New Stock Quantity: ");
                int newStockQuantity = int.Parse(Console.ReadLine()); // Consider validation
                Console.Write("New Price: ");
                decimal newPrice = decimal.Parse(Console.ReadLine()); // Consider validation
                Console.Write("New Producer Name: ");
                string newProducerName = Console.ReadLine().Trim();
                Console.Write("New Category Name: ");
                string newCategoryName = Console.ReadLine().Trim();
                Console.Write("New Additional Text: ");
                string newAdditionalText = Console.ReadLine().Trim();

                productController.EditProduct(editBarcode, newName, newStockQuantity, newPrice, newProducerName, newCategoryName, newAdditionalText);
                Console.WriteLine("\nProduct updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

    }
}
