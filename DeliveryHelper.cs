
public static class DeliveryHelper
{
    public static void PrintShipmentDetails(Shipment? shipment)
    {
        if (shipment == null)
        {
            return;
        }

        shipment.PrintShipment();
    }
}
