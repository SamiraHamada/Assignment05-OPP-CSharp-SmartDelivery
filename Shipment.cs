
public abstract partial class Shipment : ITrackable
{
    

   
    private static int TotalShipmentsCreated;

    
    static Shipment()
    {
        TotalShipmentsCreated = 0;
        Console.WriteLine("Shipment System Initialized");
    }

    public static int GetTotalShipmentsCreated()
    {
        return TotalShipmentsCreated;
    }

   

    private string trackingCode = "Unknown";
    private string description = "Unknown";
    private decimal weight = 1;
    private decimal deliveryFee = 50;
    private DeliveryAddress destination = new DeliveryAddress();

  


   
    public string TrackingCode
    {
        get { return trackingCode; }
        private set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                trackingCode = value.Trim();
            }
        }
    }

    public string Description
    {
        get { return description; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                description = value.Trim();
            }
        }
    }

    public decimal Weight
    {
        get { return weight; }
        set
        {
            if (value > 0)
            {
                weight = value;
            }
        }
    }

    public decimal DeliveryFee
    {
        get { return deliveryFee; }
        private set
        {
            // Must be greater than 0, otherwise keep the previous value
            if (value > 0)
            {
                deliveryFee = value;
            }
        }
    }

    public DeliveryAddress Destination
    {
        get { return destination; }
        set
        {
            // Protective code: never allow a null address
            if (value != null)
            {
                destination = value;
            }
        }
    }

    public abstract decimal EstimatedCost { get; }

    
    public abstract string ShipmentType { get; }



    protected Shipment(string trackingCode)
        : this(trackingCode, "Unknown", 1, 50, new DeliveryAddress())
    {
    }


    protected Shipment(
        string trackingCode,
        string description,
        decimal weight,
        decimal deliveryFee,
        DeliveryAddress destination)
    {
        TrackingCode = trackingCode;
        Description = description;
        Weight = weight;
        DeliveryFee = deliveryFee;
        Destination = destination;
        TotalShipmentsCreated++;
    }

    


   

    public void UpdateDeliveryFee(decimal newFee)
    {
        if (newFee > 0)
        {
            DeliveryFee = newFee;
        }
    }

    
    public void UpdateWeight(decimal newWeight)
    {
        if (newWeight > 0)
        {
            Weight = newWeight;
        }
    }

    
    public void UpdateWeight(decimal newWeight, decimal packingWeight)
    {
        if (newWeight > 0 && packingWeight >= 0)
        {
            Weight = newWeight + packingWeight;
        }
    }

    public abstract void PrintShipment();

    protected void PrintCommonInfo()
    {
        Console.WriteLine($"Tracking Code : {TrackingCode}");
        Console.WriteLine($"Description : {Description}");
        Console.WriteLine($"Weight : {Weight} KG");
        Console.WriteLine($"Delivery Fee : {DeliveryFee} EGP");
        Console.WriteLine($"Destination : {Destination.GetFullAddress()}");
    }

  




    // Real copy of the current shipment (independent from the original)
    public Shipment CopyShipment()
    {
        return DeepCopy();
    }

    // Shallow Copy: new Shipment object, but Destination
    // still points to the SAME DeliveryAddress object.
    // Note: MemberwiseClone() does not call the constructor,
    // so the copy is NOT counted in TotalShipmentsCreated.
    public Shipment ShallowCopy()
    {
        return (Shipment)MemberwiseClone();
    }

    // Deep Copy: new Shipment object AND new DeliveryAddress object.
    public Shipment DeepCopy()
    {
        Shipment copy = (Shipment)MemberwiseClone();
        copy.destination = Destination.Clone();
        return copy;
    }

 
}
