
public static class ShipmentFactory
{
    public static StandardShipment CreateStandardShipment()
    {
        string trackingCode = InputHelper.ReadRequiredString("Tracking Code: ");
        string description = InputHelper.ReadRequiredString("Description: ");
        decimal weight = InputHelper.ReadPositiveDecimal("Weight: ");
        decimal deliveryFee = InputHelper.ReadPositiveDecimal("Delivery Fee: ");
        DeliveryAddress address = InputHelper.ReadAddress();

        return new StandardShipment(
            trackingCode,
            description,
            weight,
            deliveryFee,
            address);
    }

    public static ExpressShipment CreateExpressShipment()
    {
        string trackingCode = InputHelper.ReadRequiredString("Tracking Code: ");
        string description = InputHelper.ReadRequiredString("Description: ");
        decimal weight = InputHelper.ReadPositiveDecimal("Weight: ");
        decimal deliveryFee = InputHelper.ReadPositiveDecimal("Delivery Fee: ");
        DeliveryAddress address = InputHelper.ReadAddress();
        decimal extraFee = InputHelper.ReadNonNegativeDecimal("Extra Fee: ");

        return new ExpressShipment(
            trackingCode,
            description,
            weight,
            deliveryFee,
            address,
            extraFee);
    }

    public static InternationalShipment CreateInternationalShipment()
    {
        string trackingCode = InputHelper.ReadRequiredString("Tracking Code: ");
        string description = InputHelper.ReadRequiredString("Description: ");
        decimal weight = InputHelper.ReadPositiveDecimal("Weight: ");
        decimal deliveryFee = InputHelper.ReadPositiveDecimal("Delivery Fee: ");
        DeliveryAddress address = InputHelper.ReadAddress();
        string destinationCountry = InputHelper.ReadRequiredString("Destination Country: ");
        decimal customsFee = InputHelper.ReadNonNegativeDecimal("Customs Fee: ");

        return new InternationalShipment(
            trackingCode,
            description,
            weight,
            deliveryFee,
            address,
            destinationCountry,
            customsFee);
    }
}
