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
        Console.WriteLine("3. Find Product by Id");
        Console.WriteLine("4. Show all Products");
        Console.WriteLine("5. Update a Product");
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
                ShowAllProducts();
                break;
            case 5:
                UpdateProduct();
                break;
            default:
                Console.WriteLine("Option not available!\nReturning to Main Menu.");
                break;
        }

    }
    }
}
