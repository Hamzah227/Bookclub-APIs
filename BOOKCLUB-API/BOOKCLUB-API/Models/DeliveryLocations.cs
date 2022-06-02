namespace BOOKCLUB_API.Models
{
    public partial class DeliveryLocations
    {
        public string Name { get; set; }
        public double Value { get; set; }
    }

    public partial class Origin
    {
        public double long_ { get; set; }
        public double lat_ { get; set; }
    }
    public partial class LocationCoordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
