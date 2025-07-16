using InventoryManager.FeatureHandler;
using InventoryManager.Models;
using InventoryManager.Parsers;
using InventoryManager.UI;
using InventoryManager.Validators;

namespace Assignments;

/// <summary>
/// The main class contains the main method, entry point of a inventory manager, c# application
/// </summary>
public class InventoryManager
{
    /// <summary>
    /// The Main method is the entry point of a inventory manager, creates a new instance of <see cref="ProductList"/>, prompts the user for actions, and handles them accordingly.
    /// </summary>
    /// <param name="args">Command line arguments</param>
    /// <returns>A task that represents the asynchronous operation of the application.</returns>
    public static async Task Main(string[] args)
    {
        ConsoleUI.CreateNewPageFor("Menu");
        ProductList list = new ProductList();
        while (true)
        {
            ConsoleUI.PromptInfo("1. Add\n2. Show\n3. Edit\n4. Delete");
            ConsoleUI.PromptInfo("5. Exit\n", ConsoleColor.Red);
            string choice = ConsoleUI.PromptAndGetInput("\nWhat do you want to do : ");
            switch (choice)
            {
                case "1":
                    FeatureHandlers.HandleAddProduct(list);
                    break;
                case "2":
                    FeatureHandlers.HandleShowProducts(list);
                    break;
                case "3":
                    FeatureHandlers.HandleEditProduct(list);
                    break;
                case "4":
                    FeatureHandlers.HandleDeleteProduct(list);
                    break;
                case "5":
                    do
                    {
                        string input = ConsoleUI.PromptAndGetInput("Closing the app will erase all product details. Are you sure you want to continue? (y/n) :", ConsoleColor.Magenta);
                        if (input.ToUpper() == "Y")
                        {
                            ConsoleUI.PromptInfo("Closing the app...", ConsoleColor.Magenta);
                            await Task.Delay(5000);
                            return;
                        }
                        else if (input.ToUpper() == "N")
                        {
                            ConsoleUI.CreateNewPageFor("Menu");
                            break;
                        }
                    }
                    while(true);
                    break;
                default:
                    Console.WriteLine("Enter a valid option");
                    break;
            }
        }
    }
}