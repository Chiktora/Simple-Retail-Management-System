using Simple_Retail_Management_System.Controllers;
using Simple_Retail_Management_System.Data.Models;

public class CategoryDisplay
{
    CategoryController categoryController;

    public CategoryDisplay()
    {
        categoryController = new CategoryController();
        CategoryInput();
    }

    public void CategoryMenu()
    {
        Console.WriteLine(new string('-', 14) + "CategoryMenu" + new string('-', 14));
        Console.WriteLine("1. Add a new Category");
        Console.WriteLine("2. Delete a Category");
        Console.WriteLine("3. Find Category by Id");
        Console.WriteLine("4. Show all Categories");
        Console.WriteLine("5. Update a Category");
        Console.WriteLine(new string('-', 40));
    }


    private void CategoryInput()
    {
        CategoryMenu();

        int operation = int.Parse(Console.ReadLine());
        switch (operation)
        {
            case 1:
                AddCategory();
                Console.WriteLine("Category added.");
                break;
            case 2:
                DeleteCategory();
                break;
            case 3:
                ShowCategory();
                break;
            case 4:
                ShowAllCategories();
                break;
            case 5:
                UpdateCategory();
                break;
            default:
                Console.WriteLine("Option not available!\nReturning to main menu...");
                break;
        }

    }

    private void AddCategory()
    {
        Category cat = new Category();
        Console.WriteLine("Category Name:");
        cat.CategoryName = Console.ReadLine();
        categoryController.Add(cat);
    }


    private void DeleteCategory()
    {
        Console.WriteLine("Enter ID to delete: ");
        int id = int.Parse(Console.ReadLine());
        Category cat = categoryController.Get(id);
        if (cat != null)
        {
            categoryController.Delete(id);
            Console.WriteLine("Done.");
        }
        else
        {
            Console.WriteLine("Category not found.");
        }
    }


    private void ShowCategory()
    {
        Console.WriteLine("Enter ID to show: ");
        int id = int.Parse(Console.ReadLine());
        Category cat = categoryController.Get(id);
        if (cat != null)
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("ID: " + cat.Id);
            Console.WriteLine("Name: " + cat.CategoryName);
        }
        else
        {
            Console.WriteLine("Category not found.");
        }
    }


    private void ShowAllCategories()
    {
        Console.WriteLine("All Categories:");
        var cats = categoryController.GetAll();
        foreach (var cat in cats)
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("ID: " + cat.Id);
            Console.WriteLine("Name: " + cat.CategoryName);
        }
    }


    private void UpdateCategory()
    {
        Console.WriteLine("Enter ID to Update: ");
        int id = int.Parse(Console.ReadLine());
        Category cat = categoryController.Get(id);
        if (cat != null)
        {
            Console.WriteLine($"Name: ");
            cat.CategoryName = Console.ReadLine();
            categoryController.Update(cat);
        }
        else
        {
            Console.WriteLine("Category not found!");
        }
    }



}
