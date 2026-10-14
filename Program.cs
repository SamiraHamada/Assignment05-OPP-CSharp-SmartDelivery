using System.Globalization;


class Program
{
    static void Main(string[] args)
    {
        
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

      

        DeliveryUtilities.PrintSystemTitle();
        Console.WriteLine();

        // First use of a static member of Shipment:
        // the static constructor runs automatically BEFORE this line is printed,
        // and no Shipment object is needed to call the static method.
        Console.WriteLine(
            $"Total Shipments Created : {Shipment.GetTotalShipmentsCreated()}");

      


        #region Driver and Delivery Center

        Console.WriteLine();

        string driverName = InputHelper.ReadRequiredString("Enter Driver Name: ");
        Driver driver = new Driver(driverName);

        string centerName = InputHelper.ReadRequiredString("Enter Delivery Center Name: ");
        DeliveryCenter center = new DeliveryCenter(centerName);
        center.Driver = driver;

        #endregion


        #region Creating Shipments

        DeliveryUtilities.PrintSectionTitle("Creating Shipments...");

        Console.WriteLine("--- Standard Shipment ---");
        StandardShipment standardShipment = ShipmentFactory.CreateStandardShipment();
        Console.WriteLine("Standard Shipment Created");

        Console.WriteLine();
        Console.WriteLine("--- Express Shipment ---");
        ExpressShipment expressShipment = ShipmentFactory.CreateExpressShipment();
        Console.WriteLine("Express Shipment Created");

        Console.WriteLine();
        Console.WriteLine("--- International Shipment ---");
        InternationalShipment internationalShipment = ShipmentFactory.CreateInternationalShipment();
        Console.WriteLine("International Shipment Created");

        // AddShipment returns false if the center is full
        bool added1 = center.AddShipment(standardShipment);
        bool added2 = center.AddShipment(expressShipment);
        bool added3 = center.AddShipment(internationalShipment);

        if (!added1 || !added2 || !added3)
        {
            Console.WriteLine("Warning: some shipments were not added to the center.");
        }

        Console.WriteLine();
        Console.WriteLine(
            $"Total Shipments Created : {Shipment.GetTotalShipmentsCreated()}");

        #endregion


        #region Object Copying

        // Assigning a variable copies only the REFERENCE, CopyShipment() creates a new object

        DeliveryUtilities.PrintSectionTitle("Object Copying");

        Shipment original = standardShipment;
        Shipment assigned = original;   // reference assignment (NOT a copy)

        Console.WriteLine($"Original Shipment  : {original.TrackingCode}");
        Console.WriteLine($"Assigned Shipment  : {assigned.TrackingCode}");
        Console.WriteLine();
        Console.WriteLine($"Same Object : {ReferenceEquals(original, assigned)}");

        // Proof: changing through 'assigned' also changes 'original'
        string oldDescription = original.Description;
        assigned.Description = "Changed through assigned variable";

        Console.WriteLine();
        Console.WriteLine("Changing description through the assigned variable...");
        Console.WriteLine($"Original Description : {original.Description}");
        Console.WriteLine($"Assigned Description : {assigned.Description}");

        original.Description = oldDescription;   // restore

        // Real copy: CopyShipment()
        Shipment realCopy = original.CopyShipment();

        Console.WriteLine();
        Console.WriteLine("Using CopyShipment()...");
        Console.WriteLine($"Same Object : {ReferenceEquals(original, realCopy)}");

        realCopy.Description = "Changed through the copy";

        Console.WriteLine($"Original Description : {original.Description}");
        Console.WriteLine($"Copied Description   : {realCopy.Description}");

        #endregion


        #region Shallow Copy

        // Shallow copy: new Shipment, but the SAME DeliveryAddress object

        DeliveryUtilities.PrintSubTitle("Shallow Copy");

        string originalCity = standardShipment.Destination.City;

        Shipment shallowCopy = standardShipment.ShallowCopy();

        Console.WriteLine($"Same Shipment Object : {ReferenceEquals(standardShipment, shallowCopy)}");
        Console.WriteLine();
        Console.WriteLine($"Original Shipment Address : {standardShipment.Destination.City}");
        Console.WriteLine($"Copied Shipment Address   : {shallowCopy.Destination.City}");

        Console.WriteLine();
        Console.WriteLine("Changing copied shipment address...");
        shallowCopy.Destination.City = "Giza";

        Console.WriteLine();
        Console.WriteLine($"Original Shipment Address : {standardShipment.Destination.City}");
        Console.WriteLine($"Copied Shipment Address   : {shallowCopy.Destination.City}");

        Console.WriteLine();
        Console.WriteLine(
            $"Same DeliveryAddress Object : " +
            $"{ReferenceEquals(standardShipment.Destination, shallowCopy.Destination)}");

        // Restore the original city before the Deep Copy demonstration
        standardShipment.Destination.City = originalCity;

        #endregion


        #region Deep Copy

        // Deep copy: new Shipment AND new DeliveryAddress object

        DeliveryUtilities.PrintSubTitle("Deep Copy");

        Shipment deepCopy = standardShipment.DeepCopy();

        Console.WriteLine($"Same Shipment Object : {ReferenceEquals(standardShipment, deepCopy)}");
        Console.WriteLine();
        Console.WriteLine($"Original Shipment Address : {standardShipment.Destination.City}");
        Console.WriteLine($"Copied Shipment Address   : {deepCopy.Destination.City}");

        Console.WriteLine();
        Console.WriteLine("Changing copied shipment address...");
        deepCopy.Destination.City = "Giza";

        Console.WriteLine();
        Console.WriteLine($"Original Shipment Address : {standardShipment.Destination.City}");
        Console.WriteLine($"Copied Shipment Address   : {deepCopy.Destination.City}");

        Console.WriteLine();
        Console.WriteLine(
            $"Same DeliveryAddress Object : " +
            $"{ReferenceEquals(standardShipment.Destination, deepCopy.Destination)}");

        // Copies created with MemberwiseClone() do not call the constructor,
        // so the static counter is still the same.
        Console.WriteLine();
        Console.WriteLine(
            $"Total Shipments Created : {Shipment.GetTotalShipmentsCreated()}");

        #endregion


        #region Tracking Status + Partial Method

        DeliveryUtilities.PrintSectionTitle("Tracking Status");

        standardShipment.UpdateTrackingStatus(TrackingStatus.InTransit);
        expressShipment.UpdateTrackingStatus(TrackingStatus.OutForDelivery);

        DeliveryUtilities.PrintSectionTitle("Partial Method");

        // OnTrackingStatusChanged() is declared in Shipment.Tracking.cs
        // and implemented in Shipment.Events.cs (same Shipment class).
        internationalShipment.UpdateTrackingStatus(TrackingStatus.Delivered);

        #endregion


        #region Extension Methods

        DeliveryUtilities.PrintSectionTitle("Extension Methods");

        Shipment[] shipments =
        {
            standardShipment,
            expressShipment,
            internationalShipment
        };

        for (int i = 0; i < shipments.Length; i++)
        {
            Console.WriteLine(shipments[i].GetSummary());
        }

        Console.WriteLine();
        Console.WriteLine(
            $"{standardShipment.TrackingCode} Is Delivered : {standardShipment.IsDelivered()}");
        Console.WriteLine(
            $"{internationalShipment.TrackingCode} Is Delivered : {internationalShipment.IsDelivered()}");

        #endregion


        #region Static Utilities

        DeliveryUtilities.PrintSectionTitle("Static Utilities");

        DeliveryUtilities.PrintSubTitle("Delivery Center");

        Console.WriteLine(
            $"Total Shipments Created : {Shipment.GetTotalShipmentsCreated()}");

        #endregion


        #region   All Shipment Details

        DeliveryUtilities.PrintSectionTitle("Delivery Center");

        center.PrintAllShipments();

        Console.WriteLine();
        DeliveryUtilities.PrintSeparator();
        Console.WriteLine("Delivery Helper");
        DeliveryUtilities.PrintSeparator();

        DeliveryHelper.PrintShipmentDetails(standardShipment);
        DeliveryHelper.PrintShipmentDetails(expressShipment);
        DeliveryHelper.PrintShipmentDetails(internationalShipment);

        #endregion


        #region ITrackable and IInsurable

        DeliveryUtilities.PrintSectionTitle("Tracking Status (ITrackable)");

        center.PrintTrackingStatuses();

        DeliveryUtilities.PrintSectionTitle("Insurance (IInsurable)");

        center.PrintInsurances();

        DeliveryUtilities.PrintSectionTitle("ITrackable[] Array");

        ITrackable[] trackableArray =
        {
            standardShipment,
            expressShipment,
            internationalShipment
        };

        for (int i = 0; i < trackableArray.Length; i++)
        {
            DeliveryReport.PrintShipment(trackableArray[i]);
        }

        DeliveryUtilities.PrintSectionTitle("IInsurable[] Array");

        IInsurable[] insurableArray =
        {
            standardShipment,
            expressShipment,
            internationalShipment
        };

        for (int i = 0; i < insurableArray.Length; i++)
        {
            DeliveryReport.PrintInsurance(insurableArray[i]);
        }

        #endregion


        #region Integer Indexer

        DeliveryUtilities.PrintSectionTitle("Integer Indexer");

        for (int i = 0; i < 3; i++)
        {
            Shipment? item = center[i];

            if (item != null)
            {
                Console.WriteLine($"Position {i} : {item.GetSummary()}");
            }
        }

        // Invalid index returns null (no exception)
        Console.WriteLine($"Position 99 is null : {center[99] == null}");

        #endregion


        #region Sealed Class and Sealed Method

        DeliveryUtilities.PrintSectionTitle("Sealed Class and Method");

        CompletedShipment completed = new CompletedShipment(
            "SH900", "Old Order", 2, 40, new DeliveryAddress("Cairo", "Tahrir", 10));

        completed.PrintShipment();

        PriorityInternationalShipment priority = new PriorityInternationalShipment(
            "SH901", "Documents", 1, 150, new DeliveryAddress("Cairo", "Nasr", 5), "France", 60);

        priority.GenerateCustomsReport();

        // class X : CompletedShipment { }            -> error: CompletedShipment is sealed
        // class Y : PriorityInternationalShipment
        // { public override void GenerateCustomsReport() { } }
        //                                            -> error: the method is sealed

        #endregion


        #region Update Weight (Method Overloading)

        DeliveryUtilities.PrintSectionTitle("Update Weight");

        Console.WriteLine($"Original Weight: {standardShipment.Weight} KG");

        standardShipment.UpdateWeight(5);
        Console.WriteLine($"Updated Weight: {standardShipment.Weight} KG");

        standardShipment.UpdateWeight(5, 0.5m);
        Console.WriteLine($"Updated Weight After Packing: {standardShipment.Weight} KG");

        #endregion


        #region Search Shipment

        DeliveryUtilities.PrintSectionTitle("Search Shipment");

        string searchCode = InputHelper.ReadRequiredString("Enter Tracking Code to search: ");

        Shipment? searchedShipment = center[searchCode];

        if (searchedShipment != null)
        {
            Console.WriteLine();
            Console.WriteLine("Shipment Found:");
            searchedShipment.PrintShipment();
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Shipment Not Found.");
        }

        #endregion


        #region Remove Shipment

        DeliveryUtilities.PrintSectionTitle("Remove Shipment");

        string removeCode = InputHelper.ReadRequiredString("Enter Tracking Code to remove: ");

        bool removed = center.RemoveShipment(removeCode);

        Console.WriteLine();
        Console.WriteLine(removed
            ? "Shipment removed successfully."
            : "Shipment Not Found.");

        DeliveryUtilities.PrintSectionTitle("Remaining Shipments");

        center.PrintAllShipments();

        #endregion


        DeliveryUtilities.PrintSectionTitle("Assignment Completed");
    }
}
