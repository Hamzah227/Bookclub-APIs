using BOOKCLUB_API.Models;
using BOOKCLUB_API.WrapperClasses;

namespace BOOKCLUB_API.IRepository
{
    public interface IGoogleMapsApi
    {
        APIModel GetDistance(DeliveryLocations destination);
        APIModel GetDirections(DeliveryLocations destination);
    }
}
