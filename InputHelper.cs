// Protective code: keeps asking until the user enters a valid value
public static class InputHelper
{
    public static string ReadRequiredString(string message)
    {
        while (true)
        {
            Console.Write(message);

            string input = Console.ReadLine() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }

            Console.WriteLine("Invalid input. Please try again.");
        }
    }

    public static decimal ReadPositiveDecimal(string message)
    {
        while (true)
        {
            Console.Write(message);

            string input = Console.ReadLine() ?? string.Empty;

            if (decimal.TryParse(input, out decimal value) && value > 0)
            {
                return value;
            }

            Console.WriteLine("Please enter a value greater than 0.");
        }
    }

    public static decimal ReadNonNegativeDecimal(string message)
    {
        while (true)
        {
            Console.Write(message);

            string input = Console.ReadLine() ?? string.Empty;

            if (decimal.TryParse(input, out decimal value) && value >= 0)
            {
                return value;
            }

            Console.WriteLine("Please enter a value of 0 or greater.");
        }
    }

    public static int ReadPositiveInt(string message)
    {
        while (true)
        {
            Console.Write(message);

            string input = Console.ReadLine() ?? string.Empty;

            if (int.TryParse(input, out int value) && value > 0)
            {
                return value;
            }

            Console.WriteLine("Please enter an integer greater than 0.");
        }
    }

    public static DeliveryAddress ReadAddress()
    {
        string city = ReadRequiredString("City: ");
        string street = ReadRequiredString("Street: ");
        int buildingNumber = ReadPositiveInt("Building Number: ");

        return new DeliveryAddress(city, street, buildingNumber);
    }
}
