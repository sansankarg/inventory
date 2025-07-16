using InventoryManager.UI;

namespace InventoryManager.Parsers;

/// <summary>
/// Provides generic parser methods works for specified data types
/// </summary>
public class Parser
{
    /// <summary>
    /// Try to parse the input string to the specified data type
    /// </summary>
    /// <param name="input">Input string</param>
    /// <param name="type">Target data type</param>
    /// <param name="result">Contains parsed if parse is successful, else the default string</param>
    /// <returns>true if parsing was successful, false if cant parsed</returns>
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
