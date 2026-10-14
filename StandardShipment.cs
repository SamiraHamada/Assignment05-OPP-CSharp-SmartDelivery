
public class StandardShipment : Shipment, IInsurable
{
    
    public StandardShipment(string trackingCode)
        : base(trackingCode)
    {
    }

    public StandardShipment(
        string trackingCode,
        string description,
        decimal weight,
        decimal deliveryFee,
        DeliveryAddress destination)
        : base(trackingCode, description, weight, deliveryFee, destination)
    {
    }

    public override string ShipmentType
    {
        get { return "Standard"; }
    }

    public override decimal EstimatedCost
    {
        get { return DeliveryFee + (Weight * 5); }
    }

 
    public decimal CalculateInsurance()
    {
        return EstimatedCost * 0.05m;
    }

    public override void PrintShipment()
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Standard Shipment");

        PrintCommonInfo();
        Console.WriteLine($"Estimated Cost : {EstimatedCost} EGP");
    }
}
