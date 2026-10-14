// Priority version of InternationalShipment (shows the sealed method)
public class PriorityInternationalShipment : InternationalShipment
{
    public PriorityInternationalShipment(
        string trackingCode,
        string description,
        decimal weight,
        decimal deliveryFee,
        DeliveryAddress destination,
        string destinationCountry,
        decimal customsFee)
        : base(
            trackingCode,
            description,
            weight,
            deliveryFee,
            destination,
            destinationCountry,
            customsFee)
    {
    }

    public override string ShipmentType
    {
        get { return "Priority International"; }
    }

    // Sealed method: classes inheriting from this one cannot override it again
    public sealed override void GenerateCustomsReport()
    {
        Console.WriteLine(
            $"Priority Customs Report: {TrackingCode} - " +
            $"{DestinationCountry} - " +
            $"Customs Fee: {CustomsFee} EGP");
    }
}
