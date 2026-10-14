// International shipment: adds country + customs fee, insurance = 12% of EstimatedCost
public class InternationalShipment : Shipment, IInsurable
{
    private string destinationCountry = "Unknown";
    private decimal customsFee = 0;

    public string DestinationCountry
    {
        get { return destinationCountry; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                destinationCountry = value.Trim();
            }
        }
    }

    public decimal CustomsFee
    {
        get { return customsFee; }
        set
        {
            if (value >= 0)
            {
                customsFee = value;
            }
        }
    }

    public InternationalShipment(
        string trackingCode,
        string description,
        decimal weight,
        decimal deliveryFee,
        DeliveryAddress destination,
        string destinationCountry,
        decimal customsFee)
        : base(trackingCode, description, weight, deliveryFee, destination)
    {
        DestinationCountry = destinationCountry;
        CustomsFee = customsFee;
    }

    public override string ShipmentType
    {
        get { return "International"; }
    }

    public override decimal EstimatedCost
    {
        get { return DeliveryFee + (Weight * 5) + CustomsFee; }
    }

    // 12% of EstimatedCost
    public decimal CalculateInsurance()
    {
        return EstimatedCost * 0.12m;
    }

    public override void PrintShipment()
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("International Shipment");

        PrintCommonInfo();
        Console.WriteLine($"Destination Country : {DestinationCountry}");
        Console.WriteLine($"Customs Fee : {CustomsFee} EGP");
        Console.WriteLine($"Estimated Cost : {EstimatedCost} EGP");
    }

    public virtual void GenerateCustomsReport()
    {
        Console.WriteLine(
            $"Customs Report: {TrackingCode} - " +
            $"{DestinationCountry} - " +
            $"Customs Fee: {CustomsFee} EGP");
    }
}
