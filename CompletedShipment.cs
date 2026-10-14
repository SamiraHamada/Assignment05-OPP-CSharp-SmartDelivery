// Sealed class: no other class can inherit from it
public sealed class CompletedShipment : Shipment
{
    public CompletedShipment(
        string trackingCode,
        string description,
        decimal weight,
        decimal deliveryFee,
        DeliveryAddress destination)
        : base(trackingCode, description, weight, deliveryFee, destination)
    {
        // A completed shipment is always Delivered
        // (set directly so no "status changed" message is printed)
        CurrentStatus = TrackingStatus.Delivered;
    }

    public override string ShipmentType
    {
        get { return "Completed"; }
    }

    public override decimal EstimatedCost
    {
        get { return DeliveryFee + (Weight * 5); }
    }

    public override void PrintShipment()
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Completed Shipment");

        PrintCommonInfo();
        Console.WriteLine($"Estimated Cost : {EstimatedCost} EGP");
    }
}
