using InventoryManager.UI;

namespace InventoryManager.Validators;

/// <summary>
/// Provides validators for product fields via a <see cref="Validate"/> method.
/// </summary>
public class Validators
{
    /// <summary>
    /// Validate the value based on the field name and will not validate if it is an unhandled field.
    /// </summary>
    /// <param name="field">Field to type to validate.</param>
    /// <param name="value">Value to validate.</param>
    /// <returns><see cref="true"/> If value passes the specific field validation or unhandled fields; otherwise <see cref="false"/>.</returns>
    public static bool Validate(string field, object? value)
    {
        if (value is null)
        {
            ConsoleUI.PromptInfo("Value cannot be null !", ConsoleColor.Yellow);
            return false;
        }

        switch (field)
        {
            case "Name":
                return ValidateName((string)value);
            case "Id":
                return ValidateId((string)value);
            case "Price":
                return ValidatePrice((int)value);
            case "Quantity":
                return ValidateQuantity((int)value);
            default:
                return true;
        }
    }

    private static bool ValidateName(string name)
    {
        if (name.Length >= 20)
        {
            ConsoleUI.PromptInfo("Name should not exceed 20 characters !", ConsoleColor.Yellow);
            return false;
        }

        return true;
    }

    private static bool ValidateId(string id)
    {
        if (id.Length != 10)
        {
            ConsoleUI.PromptInfo("Id must have 10 characters ! ", ConsoleColor.Yellow);
            return false;
        }

        return true;
    }

    private static bool ValidatePrice(int price)
    {
        if (price < 0)
        {
            ConsoleUI.PromptInfo("Price cannot be a negative value !", ConsoleColor.Yellow);
            return false;
        }

        return true;
    }

    private static bool ValidateQuantity(int quantity)
    {
        if (quantity < 0)
        {
            ConsoleUI.PromptInfo("Atleast one quantity is required !", ConsoleColor.Yellow);
            return false;
        }

        return true;
    }
}
