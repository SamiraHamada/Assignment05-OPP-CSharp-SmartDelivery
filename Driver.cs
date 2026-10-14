// Driver assigned to a DeliveryCenter (name is validated)
public class Driver
{
    public string Name { get; set; }

    public Driver(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            Name = "Unknown";
        }
        else
        {
            Name = name.Trim();
        }
    }
}
