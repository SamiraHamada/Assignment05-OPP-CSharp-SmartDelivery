
public static class DeliveryUtilities
{
    private const int LineLength = 42;

    public static void PrintSeparator(char symbol = '=')
    {
        Console.WriteLine(new string(symbol, LineLength));
    }

    public static void PrintSystemTitle()
    {
        PrintSeparator();
        Console.WriteLine("Smart Delivery Management System");
        PrintSeparator();
    }

    // Prints:  ====== / title / ======
    public static void PrintSectionTitle(string title)
    {
        Console.WriteLine();
        PrintSeparator();
        Console.WriteLine(title);
        PrintSeparator();
        Console.WriteLine();
    }

  
    public static void PrintSubTitle(string title)
    {
        Console.WriteLine();
        PrintSeparator('-');
        Console.WriteLine(title);
        PrintSeparator('-');
        Console.WriteLine();
    }
}
