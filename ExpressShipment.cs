// Express shipment: adds ExtraFee to the cost, insurance = 8% of EstimatedCost
public class ExpressShipment : Shipment, IInsurable
{
    private decimal extraFee = 0;

    public decimal ExtraFee
    {
        get { return extraFee; }
        set
        {
            if (value >= 0)
            {
                extraFee = value;
            }
        }
    }

    public ExpressShipment(
        string trackingCode,
        string description,
        decimal weight,
        decimal deliveryFee,
        DeliveryAddress destination,
        decimal extraFee)
        : base(trackingCode, description, weight, deliveryFee, destination)
    {
        ExtraFee = extraFee;
    }

    public override string ShipmentType
    {
        get { return "Express"; }
    }

    public override decimal EstimatedCost
    {
        get { return DeliveryFee + (Weight * 5) + ExtraFee; }
    }

    // 8% of EstimatedCost
    public decimal CalculateInsurance()
    {
        return EstimatedCost * 0.08m;
    }

    public override void PrintShipment()
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Express Shipment");

        PrintCommonInfo();
        Console.WriteLine($"Extra Fee : {ExtraFee} EGP");
        Console.WriteLine($"Estimated Cost : {EstimatedCost} EGP");
    }
}
