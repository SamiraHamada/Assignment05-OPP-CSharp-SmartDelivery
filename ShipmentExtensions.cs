
public static class ShipmentExtensions
{
   
    public static string GetSummary(this Shipment shipment)
    {
        if (shipment == null)
        {
            return string.Empty;
        }

        return $"{shipment.TrackingCode} | " +
               $"{shipment.ShipmentType} | " +
               $"{shipment.Weight:0.##} KG | " +
               $"{shipment.StatusText}";
    }

    public static bool IsDelivered(this Shipment shipment)
    {
        if (shipment == null)
        {
            return false;
        }

        return shipment.CurrentStatus == TrackingStatus.Delivered;
    }
}
