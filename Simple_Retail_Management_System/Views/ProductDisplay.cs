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
        Console.WriteLine(new string('-', 14) + "ProductsMenu" + new string('-', 14));
        Console.WriteLine("1. Add a new Product");
        Console.WriteLine("2. Delete a Product");
        Console.WriteLine("3. Find Product by barcode");
        Console.WriteLine("4. Update a Product");
        Console.WriteLine("5. Show products by producer");
        Console.WriteLine("6. Show products by category");
        Console.WriteLine(new string('-', 40));
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
            Console.Write("Enter the name of the category to get products from: ");
            string categoryName = Console.ReadLine();

            List<Product> productsByCategory = productController.GetProductsByCategory(categoryName);

            if (productsByCategory.Any())
            {
                Console.WriteLine($"Products in the '{categoryName}' category:");
                foreach (var product in productsByCategory)
                {
                    Console.WriteLine($"Product Name: {product.Name}, Barcode: {product.Barcode}, Price: {product.Price}");
                }
            }
            else
            {
                Console.WriteLine($"No products found in the '{categoryName}' category.");
            }
        }

        private void ShowProductsByProducer()
        {
            Console.Write("Enter the name of the producer to get their products: ");
            string producerName = Console.ReadLine();

            List<Product> productsByProducer = productController.GetProductsByProducer(producerName);

            if (productsByProducer.Any())
            {
                Console.WriteLine($"Products by {producerName}:");
                foreach (var product in productsByProducer)
                {
                    Console.WriteLine($"Product Name: {product.Name}, Barcode: {product.Barcode}, Price: {product.Price}");
                }
            }
            else
            {
                Console.WriteLine($"No products found for producer {producerName}.");
            }
        }

        private void AddProduct()
        {
            try
            {
                Product prod = new Product();

                Console.WriteLine("Enter product details:");
                Console.Write("Barcode: ");
                string barcode = Console.ReadLine();
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Stock Quantity: ");
                int stockQuantity = int.Parse(Console.ReadLine());
                Console.Write("Price: ");
                decimal price = decimal.Parse(Console.ReadLine());
                Console.Write("Producer Name: ");
                string producerName = Console.ReadLine();
                Console.Write("Category Name: ");
                string categoryName = Console.ReadLine();
                Console.Write("Additional Text: ");
                string additionalText = Console.ReadLine();
                productController.AddProduct(barcode, name, stockQuantity, price, producerName, categoryName, additionalText);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        
    }
    private void DeleteProduct()
    {
            try
            {
                Console.Write("Enter barcode of the product to delete: ");
                string deleteBarcode = Console.ReadLine();
                productController.DeleteProduct(deleteBarcode);
                Console.WriteLine("Product deleted successfully.");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }

    }
     private void ShowProduct()
    {
         Console.Write("Enter barcode of the product to read: ");
                string readBarcode = Console.ReadLine();
                Product product = productController.ReadProduct(readBarcode);
            Console.WriteLine($"Product Name: {product.Name},\nQuantity: {product.StockQuantity},\nPrice: {product.Price}");

        }
        
        private void UpdateProduct()
        {
            try
            {
                Console.WriteLine("Enter product details to update:");
                Console.Write("Barcode: ");
                string editBarcode = Console.ReadLine();
                Console.Write("New Name: ");
                string newName = Console.ReadLine();
                Console.Write("New Stock Quantity: ");
                int newStockQuantity = int.Parse(Console.ReadLine());
                Console.Write("New Price: ");
                decimal newPrice = decimal.Parse(Console.ReadLine());
                Console.Write("New Producer Name: ");
                string newProducerName = Console.ReadLine();
                Console.Write("New Category Name: ");
                string newCategoryName = Console.ReadLine();
                Console.Write("New Additional Text: ");
                string newAdditionalText = Console.ReadLine();
                productController.EditProduct(editBarcode, newName, newStockQuantity, newPrice, newProducerName, newCategoryName, newAdditionalText);
                Console.WriteLine("Product updated successfully.");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
             
    }
    }
}
