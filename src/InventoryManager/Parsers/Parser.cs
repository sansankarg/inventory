using InventoryManager.UI;

namespace InventoryManager.Parsers;

/// <summary>
/// Provides universal parser methods works for all data types
/// </summary>
public class Parser
{
    /// <summary>
    /// Try to parse the value according to data type
    /// </summary>
    /// <param name="input">Input string</param>
    /// <param name="type">Type of the object</param>
    /// <param name="result">Parsed value</param>
    /// <returns>true if can parse, false if cant parsed</returns>
    public static bool TryParseValue(string input, Type type, out object result)
    {
        bool status = false;
        if (type == typeof(int))
        {
            status = int.TryParse(input, out int number);
            result = number;
            if (!status)
            {
                ConsoleUI.PromptInfo("Give a valid input!", ConsoleColor.Yellow);
            }

            return status;
        }
        else if (type == typeof(string))
        {
            result = input;
            return true;
        }

        result = input;
        return status;
    }
}
