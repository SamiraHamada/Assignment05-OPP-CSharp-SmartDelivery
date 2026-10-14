// Shipment.Tracking.cs  (tracking part of the partial class)
public abstract partial class Shipment
{
    public TrackingStatus CurrentStatus { get; protected set; } = TrackingStatus.Ready;

    public string StatusText
    {
        get
        {
            switch (CurrentStatus)
            {
                case TrackingStatus.Ready:
                    return "Ready";
                case TrackingStatus.InTransit:
                    return "In Transit";
                case TrackingStatus.OutForDelivery:
                    return "Out For Delivery";
                case TrackingStatus.Delivered:
                    return "Delivered";
                default:
                    return "Unknown";
            }
        }
    }

    // ITrackable implementation
    public string GetTrackingStatus()
    {
        switch (CurrentStatus)
        {
            case TrackingStatus.Ready:
                return $"Shipment {TrackingCode} is Ready.";
            case TrackingStatus.InTransit:
                return $"Shipment {TrackingCode} is In Transit.";
            case TrackingStatus.OutForDelivery:
                return $"Shipment {TrackingCode} is Out for Delivery.";
            case TrackingStatus.Delivered:
                return $"Shipment {TrackingCode} has been Delivered.";
            default:
                return $"Shipment {TrackingCode} has an unknown status.";
        }
    }

    public void UpdateTrackingStatus(TrackingStatus newStatus)
    {
        
        if (!Enum.IsDefined(typeof(TrackingStatus), newStatus))
        {
            return;
        }

        if (newStatus == CurrentStatus)
        {
            return;
        }

        CurrentStatus = newStatus;

        // Partial method call
        OnTrackingStatusChanged(StatusText);
    }

   
    partial void OnTrackingStatusChanged(string newStatus);
}
