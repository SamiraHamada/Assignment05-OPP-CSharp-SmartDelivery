
using System.Globalization;

public static class DeliveryReport
{
    // Prints the tracking status of ANY shipment
    public static void PrintShipment(ITrackable? shipment)
    {
        if (shipment == null)
        {
            return;
        }

        Console.WriteLine(shipment.GetTrackingStatus());
    }

    // Prints the insurance cost of ANY insurable shipment
    public static void PrintInsurance(IInsurable? shipment)
    {
        if (shipment == null)
        {
            return;
        }

        string type = "Shipment";

        if (shipment is Shipment realShipment)
        {
            type = realShipment.ShipmentType + " Shipment";
        }

        decimal insurance = shipment.CalculateInsurance();

        Console.WriteLine(
            $"{type} Insurance : " +
            $"{insurance.ToString("F2", CultureInfo.InvariantCulture)} EGP");
    }
}
