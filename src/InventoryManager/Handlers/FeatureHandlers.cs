namespace InventoryManager.FeatureHandler;
using InventoryManager.Models;
using InventoryManager.Parsers;
using InventoryManager.UI;
using InventoryManager.Validators;
using System;

/// <summary>
/// Provides methods to handle features of the inventory manager.
/// </summary>
internal class FeatureHandlers
{
    /// <summary>
    /// Handle getting user inputs and addition of a new product to the inventory.
    /// </summary>
    /// <remarks>
    /// This method prompt the user to enter all the details for the product
    /// and create a new product with the user input details in product inventory.
    /// </remarks>
    /// <param name="list">Give access to user list</param>
    public static void HandleAddProduct(ProductList list)
    {
        ConsoleUI.CreateNewPageFor("Add");
        ConsoleUI.PromptInfo("Enter the details of product :");
        Dictionary<string, object>? newProductDetails = new Dictionary<string, object>();
        Dictionary<string, Type>? productTemplate = Product.GetTemplate();
        foreach (var field in productTemplate)
        {
            object result;
            string input;
            do
            {
                input = ConsoleUI.PromptAndGetInput($"{field.Key} : ");
            }
            while (!(Parser.TryParseValue(input, field.Value, out result) && Validators.Validate(field.Key, result)));
            newProductDetails[field.Key] = result;
        }

        Product newProduct = new Product(newProductDetails);
        list.Add(newProduct);
        ConsoleUI.PromptInfo("Added successfully", ConsoleColor.Green);
        ConsoleUI.WaitAndNavigateToMenu();
    }

    /// <summary>
    /// Handles showing the products in the inventory to the user.
    /// </summary>
    /// <remarks>
    /// This method list all the products as a table format to the user. It informs user
    /// if product list was empty and ask user to return back to menu.
    /// </remarks>
    /// <param name="list">Give access to user list</param>
    public static void HandleShowProducts(ProductList list)
    {
        ConsoleUI.CreateNewPageFor("Show");
        if (!ShowProducts(list))
        {
            return;
        }
        ConsoleUI.WaitAndNavigateToMenu();
        return;
    }

    /// <summary>
    /// Handles editing a product in the inventory by getting user input and applying the changes.
    /// </summary>
    /// <remarks> This method list all the products as a table format to the user and prompt the user to
    /// select a index of product to edit (It informs user if product list was empty and ask user to return
    /// back to menu). It list the available
    /// fields of the product and prompt the user to select the field to edit. Then it prompt the user to
    /// enter the new value to change and apply changes to the list. This function validates the index, field,
    /// and new value of the field with validators.
    /// </remarks>
    /// <param name="list">Give access to user list</param>
    public static void HandleEditProduct(ProductList list)
    {
        ConsoleUI.CreateNewPageFor("Edit");
        if (!ShowProducts(list))
        {
            return;
        }

        int index = GetIndex(list);
        string field = GetFieldName(list);
        object value;
        string valueString;
        do
        {
            valueString = ConsoleUI.PromptAndGetInput($"New {field} : ");
        }
        while (!(Parser.TryParseValue(valueString, Product.GetTemplate()[field], out value) && Validators.Validate(field, value)));
        list.Edit(index - 1, field, value);
        ConsoleUI.PromptInfo("Edited successfully", ConsoleColor.Green);
        ConsoleUI.WaitAndNavigateToMenu();
        return;
    }

    /// <summary>
    /// Handles the deletion of a product from the provided product list.
    /// </summary>
    /// <remarks>This method list the products as table format with index to the user and prompt the user
    /// to select a index of product to delete. If no products are in the list, then it inform the user
    /// that there is products in the list so navigate back to menu. It also check the input is a valid index.
    /// </remarks>
    /// <param name="list">The <see cref="ProductList"/> instance containing the products to manage.</param>
    public static void HandleDeleteProduct(ProductList list)
    {
        ConsoleUI.CreateNewPageFor("Delete");
        if (!ShowProducts(list))
        {
            return;
        }

        list.Delete(GetIndex(list) - 1);
        ConsoleUI.PromptInfo("Deleted successfully", ConsoleColor.Green);
        ConsoleUI.WaitAndNavigateToMenu();
        return;
    }

    private static int GetIndex(ProductList list)
    {
        int index;
        do
        {
            string indexString = ConsoleUI.PromptAndGetInput("Enter the index of the product : ");
            if (!int.TryParse(indexString, out index))
            {
                ConsoleUI.PromptInfo("Enter a valid index", ConsoleColor.Yellow);
                continue;
            }

            if (index > list.Count() || index <= 0)
            {
                ConsoleUI.PromptInfo("No product exist at the given index " + index, ConsoleColor.Yellow);
                continue;
            }

            return index;
        }
        while (true);
    }

    private static string GetFieldName(ProductList list)
    {
        do
        {
            string[] fields = Product.GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                Console.Write($"{i + 1} {fields[i]} ");
            }

            string fieldChoiceString = ConsoleUI.PromptAndGetInput("Enter a field to edit : ");
            if (!int.TryParse(fieldChoiceString, out int fieldChoice) && fieldChoice < fields.Length && fieldChoice > 0)
            {
                ConsoleUI.PromptInfo("Enter a valid choice", ConsoleColor.Yellow);
                continue;
            }

            return fields[fieldChoice - 1];
        }
        while (true);
    }

    /// <summary>
    /// Will list all the products as table.
    /// </summary>
    /// <param name="list">Give access to user list.</param>
    /// <returns><see cref="false"/> if list is empty; otherwies <see cref="true"/></returns>
    private static bool ShowProducts(ProductList list)
    {
        List<Product>? currentProductList = list.Get();
        if (currentProductList is null || currentProductList.Count == 0)
        {
            ConsoleUI.PromptInfo("No products found.", ConsoleColor.Yellow);
            ConsoleUI.WaitAndNavigateToMenu();
            return false;
        }

        ConsoleUI.PrintAsTable(currentProductList);
        return true;
    }
}
