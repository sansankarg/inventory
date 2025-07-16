﻿using ConsoleTables;
using InventoryManager.Models;

namespace InventoryManager.UI;

/// <summary>
/// Has functions related to console manipulations
/// </summary>
public class ConsoleUI
{
    /// <summary>
    /// Prompt the user and returns the user input for the prompt
    /// </summary>
    /// <param name="prompt">Prompt shown to the user</param>
    /// <param name="color">Color for the prompt.</param>
    /// <returns>User input</returns>
    public static string PromptAndGetInput(string prompt, ConsoleColor color = ConsoleColor.White)
    {
        do
        {
            Console.ForegroundColor = color;
            Console.Write(prompt);
            Console.ResetColor();
            string? input = Console.ReadLine();
            if (input is null || input.Trim() == string.Empty)
            {
                Console.WriteLine("Input cannot be empty. Please try again.");
                continue;
            }
            else
            {
                return input.Trim();
            }
        }
        while (true);
    }

    /// <summary>
    /// Prompt the user with info with color.
    /// </summary>
    /// <param name="info">Info to shown</param>
    /// <param name="color">Color for the prompt.</param>
    public static void PromptInfo(string info, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(info);
        Console.ResetColor();
    }

    /// <summary>
    /// Prints <see cref="List{Product}"/> as a table in the console.
    /// </summary>
    /// <param name="currentProductList">Product list to print</param>
    public static void PrintAsTable(List<Product> currentProductList)
    {
        Dictionary<string, Type> productTemplate = Product.GetTemplate();
        string[] fields = new string[] { "Index" }.Concat(productTemplate.Keys.ToArray()).ToArray();
        ConsoleTable table = new ConsoleTable(fields);
        for (int index = 0; index < currentProductList.Count; index++)
        {
            object[] values = new object[fields.Length];
            values[0] = index + 1;
            for (int i = 1; i < fields.Length; i++)
            {
                values[i] = currentProductList[index][fields[i]];
            }

            table.AddRow(values);
        }

        table.Write();
    }

    /// <summary>
    /// Wait for the keypress and clear the console to navigate back to the menu.
    /// </summary>
    public static void WaitAndNavigateToMenu()
    {
        Console.WriteLine("\n\n\nPress any key to leave to menu...");
        Console.ReadKey();
        ConsoleUI.CreateNewPageFor("Menu");
    }

    /// <summary>
    /// Clears the console and create new page for the actions
    /// </summary>
    /// <param name="action">Current action</param>
    public static void CreateNewPageFor(string action)
    {
        Console.Clear();
        PromptInfo($"Inventory manager - {action}", ConsoleColor.DarkBlue);
        Console.WriteLine();
    }
}
