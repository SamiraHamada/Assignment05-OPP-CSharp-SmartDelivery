
public class DeliveryCenter
{
    private const int MaxShipments = 20;

    private readonly Shipment?[] shipments = new Shipment?[MaxShipments];
    private string centerName = "Unknown";

    public string CenterName
    {
        get { return centerName; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                centerName = value.Trim();
            }
        }
    }

    public Driver? Driver { get; set; }

    // Number of shipments currently stored
    public int Count
    {
        get
        {
            int count = 0;

            for (int i = 0; i < shipments.Length; i++)
            {
                if (shipments[i] != null)
                {
                    count++;
                }
            }

            return count;
        }
    }

    public DeliveryCenter(string centerName)
    {
        CenterName = centerName;
    }

    // Integer indexer: invalid index -> get returns null, set does nothing
    public Shipment? this[int index]
    {
        get
        {
            if (index < 0 || index >= shipments.Length)
            {
                return null;
            }

            return shipments[index];
        }
        set
        {
            if (index >= 0 && index < shipments.Length)
            {
                shipments[index] = value;
            }
        }
    }

    // String indexer: first shipment with this tracking code (null if not found)
    public Shipment? this[string? trackingCode]
    {
        get
        {
            int index = FindIndex(trackingCode);

            if (index < 0)
            {
                return null;
            }

            return shipments[index];
        }
    }

    // Adds the shipment in the first empty position.
    // Returns false if the center is full (or the shipment is invalid / duplicated).
    public bool AddShipment(Shipment? shipment)
    {
        if (shipment == null)
        {
            return false;
        }

        if (FindIndex(shipment.TrackingCode) >= 0)
        {
            Console.WriteLine(
                $"Shipment {shipment.TrackingCode} already exists in the center.");
            return false;
        }

        for (int i = 0; i < shipments.Length; i++)
        {
            if (shipments[i] == null)
            {
                shipments[i] = shipment;
                return true;
            }
        }

        return false;
    }

    // Searches by tracking code and removes the shipment if found
    public bool RemoveShipment(string? trackingCode)
    {
        int index = FindIndex(trackingCode);

        if (index < 0)
        {
            return false;
        }

        shipments[index] = null;
        return true;
    }

    // Dynamic binding: each object runs its own PrintShipment()
    public void PrintAllShipments()
    {
        Console.WriteLine($"Delivery Center : {CenterName}");

        if (Driver != null)
        {
            Console.WriteLine($"Driver : {Driver.Name}");
        }

        if (Count == 0)
        {
            Console.WriteLine("No shipments in this center.");
            return;
        }

        for (int i = 0; i < shipments.Length; i++)
        {
            Shipment? shipment = shipments[i];

            if (shipment != null)
            {
                shipment.PrintShipment();
            }
        }
    }

    // Prints the tracking status of every shipment using the ITrackable interface
    public void PrintTrackingStatuses()
    {
        for (int i = 0; i < shipments.Length; i++)
        {
            ITrackable? trackable = shipments[i];

            if (trackable != null)
            {
                DeliveryReport.PrintShipment(trackable);
            }
        }
    }

    // Prints the insurance of every shipment that implements IInsurable
    public void PrintInsurances()
    {
        for (int i = 0; i < shipments.Length; i++)
        {
            if (shipments[i] is IInsurable insurable)
            {
                DeliveryReport.PrintInsurance(insurable);
            }
        }
    }

    // Returns the position of the tracking code, or -1 if not found
    private int FindIndex(string? trackingCode)
    {
        if (string.IsNullOrWhiteSpace(trackingCode))
        {
            return -1;
        }

        for (int i = 0; i < shipments.Length; i++)
        {
            Shipment? shipment = shipments[i];

            if (shipment != null
                && string.Equals(
                    shipment.TrackingCode,
                    trackingCode.Trim(),
                    StringComparison.OrdinalIgnoreCase))
            {
                return i;
            }
        }

        return -1;
    }
}
