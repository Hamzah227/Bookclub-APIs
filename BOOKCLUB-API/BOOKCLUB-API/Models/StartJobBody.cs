namespace BOOKCLUB_API.Models
{
    public class StartJobBody
    {
        public int RiderId { get; set; }
        public int RequestDeliveryId { get; set; }
        public string RiderLocationName { get; set; }
        public LocationCoordinates RiderLocation { get; set; }
    }
}
