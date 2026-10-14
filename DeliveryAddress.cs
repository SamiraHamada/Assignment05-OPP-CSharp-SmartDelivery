
public class DeliveryAddress
{
    private string city = "Unknown";
    private string street = "Unknown";
    private int buildingNumber = 1;

    public string City
    {
        get { return city; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                city = value.Trim();
            }
        }
    }

    public string Street
    {
        get { return street; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                street = value.Trim();
            }
        }
    }

    public int BuildingNumber
    {
        get { return buildingNumber; }
        set
        {
            if (value > 0)
            {
                buildingNumber = value;
            }
        }
    }

    public DeliveryAddress()
    {
    }

    public DeliveryAddress(
        string city,
        string street,
        int buildingNumber)
    {
        City = city;
        Street = street;
        BuildingNumber = buildingNumber;
    }

    public string GetFullAddress()
    {
        return $"{BuildingNumber} {Street}, {City}";
    }

    // Creates a brand new DeliveryAddress object (used by Deep Copy)
    public DeliveryAddress Clone()
    {
        return new DeliveryAddress(City, Street, BuildingNumber);
    }
}
