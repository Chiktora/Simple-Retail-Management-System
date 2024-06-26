﻿using Simple_Retail_Management_System.Controllers;
using Simple_Retail_Management_System.Data.Models;

public class CategoryDisplay
{
    CategoryController categoryController;

    public CategoryDisplay()
    {
        categoryController = new CategoryController();
        CategoryInput();
    }
        /// <summary>
        /// Prints out a menu for category
        /// </summary>
    public void CategoryMenu()
    {
        Console.Clear();
        Console.WriteLine("╔" + new string('═', 14) + "CategoryMenu" + new string('═', 14) + "╗");
        Console.WriteLine("║ 1. Add a new Category                  ║");
        Console.WriteLine("║ 2. Delete a Category                   ║");
        Console.WriteLine("║ 3. Find Category by Id                 ║");
        Console.WriteLine("║ 4. Show all Categories                 ║");
        Console.WriteLine("║ 5. Update a Category                   ║");
        Console.WriteLine("║ 6. Exit Category Menu                  ║");
        Console.WriteLine("╚" + new string('═', 40) + "╝");

    }

        /// <summary>
        /// Enter a number based on the menu and it executes the corresponding method
        /// </summary>
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
                Console.WriteLine("Option not available!\nReturning to Main Menu.");
                break;
        }

    }
        /// <summary>
        /// Adds a new category based on the inputs of the user
        /// </summary>
    private void AddCategory()
    {
        Console.Clear();
        DisplayHeader("Add New Category");

        Category cat = new Category();

        Console.Write("Category Name: ");
        cat.CategoryName = Console.ReadLine().Trim();

        categoryController.Add(cat);
        Console.WriteLine("\nCategory added successfully.");

        PromptContinue();
    }


        /// <summary>
        /// Deletes a category by the ID given
        /// </summary>
    private void DeleteCategory()
    {
        Console.Clear();
        DisplayHeader("Delete Category");

        Console.Write("Enter ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Category cat = categoryController.Get(id);
            if (cat != null)
            {
                categoryController.Delete(id);
                Console.WriteLine("\nCategory deleted.");
            }
            else
            {
                Console.WriteLine("\nCategory not found.");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid input. Please enter a numeric ID.");
        }

        PromptContinue();
    }


        /// <summary>
        /// Prints out the properties of a category by ID
        /// </summary>
    private void ShowCategory()
    {
        Console.Clear();
        DisplayHeader("Show Category Details");

        Console.Write("Enter ID to show: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Category cat = categoryController.Get(id);
            if (cat != null)
            {
                Console.WriteLine(new string('-', 40));
                Console.WriteLine("ID: " + cat.Id);
                Console.WriteLine("Name: " + cat.CategoryName);
                Console.WriteLine(new string('-', 40));
            }
            else
            {
                Console.WriteLine("\nCategory not found.");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid input. Please enter a numeric ID.");
        }

        PromptContinue();
    }


        /// <summary>
        /// Prints out the properties of all categories
        /// </summary>
    private void ShowAllCategories()
    {
        Console.Clear();
        DisplayHeader("All Categories");

        var cats = categoryController.GetAll();
        foreach (var cat in cats)
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("ID: " + cat.Id);
            Console.WriteLine("Name: " + cat.CategoryName);
            Console.WriteLine(new string('-', 40));
        }

        PromptContinue();
    }


        /// <summary>
        /// Updates the properties of a category by ID
        /// </summary>
    private void UpdateCategory()
    {
        Console.Clear();
        DisplayHeader("Update Category");

        Console.Write("Enter ID to Update: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Category cat = categoryController.Get(id);
            if (cat != null)
            {
                Console.Write("New Name: ");
                cat.CategoryName = Console.ReadLine().Trim();

                categoryController.Update(cat);
                Console.WriteLine("\nCategory updated successfully.");
            }
            else
            {
                Console.WriteLine("\nCategory not found.");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid input. Please enter a numeric ID.");
        }

        PromptContinue();
    }
    private void DisplayHeader(string title)
    {
        Console.WriteLine("╔" + new string('═', 50) + "╗");
        Console.WriteLine("║" + title.PadRight(50) + "║");
        Console.WriteLine("╚" + new string('═', 50) + "╝");
    }

    private void DisplayCustomerDetails(Customer cus)
    {
        Console.WriteLine(new string('-', 40));
        Console.WriteLine($"ID: {cus.Id}");
        Console.WriteLine($"Name: {cus.Name}");
        Console.WriteLine($"Phone Number: {cus.PhoneNumber}");
        Console.WriteLine($"Email: {cus.Email}");
        Console.WriteLine(new string('-', 40));
    }

    private void PromptContinue()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }




}
