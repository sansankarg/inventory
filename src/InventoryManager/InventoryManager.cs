using InventoryManager.FeatureHandler;
using InventoryManager.Models;
using InventoryManager.UI;

namespace Assignments;

/// <summary>
/// The main class contains the main method, entry point of a inventory manager, c# application
/// </summary>
public class InventoryManager
{
    /// <summary>
    /// The Main method is the entry point of the inventory manager, creates a new instance of <see cref="ProductList"/>, prompts the user for actions, and handles them accordingly.
    /// </summary>
    /// <param name="args">Command line arguments</param>
    /// <returns>A task that represents the asynchronous operation of the application.</returns>
    public static async Task Main(string[] args)
    {
        ConsoleUI.CreateNewPageFor("Menu");
        ProductList list = new ();
        while (true)
        {
            ConsoleUI.PromptInfo("1. Add Product\n2. Show Product\n3. Edit Product\n4. Delete Product");
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
                    bool confirmExit = await ConfirmExitAsync();
                    if (!confirmExit)
                    {
                        return;
                    }
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private static async Task<bool> ConfirmExitAsync()
    {
        do
        {
            string input = ConsoleUI.PromptAndGetInput("Warning : Closing the app will erase all product details. Are you sure you want to continue? (y/n) :", ConsoleColor.Magenta);
            if (input.ToUpper() == "Y")
            {
                ConsoleUI.PromptInfo("Closing the app...", ConsoleColor.Magenta);
                await Task.Delay(1000);
                return false;
            }
            else if (input.ToUpper() == "N")
            {
                ConsoleUI.CreateNewPageFor("Menu");
                break;
            }
        }
        while (true);
        return true;
    }
}