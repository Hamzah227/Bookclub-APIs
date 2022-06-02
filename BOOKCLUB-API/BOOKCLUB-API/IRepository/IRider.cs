using BOOKCLUB_API.Models;
using BOOKCLUB_API.WrapperClasses;

namespace BOOKCLUB_API.IRepository
{
    public interface IRider
    {
        APIModel AcceptDeliveryRequest(RiderAcceptRequest request);
        APIModel ReachedSource(RequestWrapperClass request);
        APIModel ReachedDestination(RequestWrapperClass request);
        APIModel SignUpRider(Rider rider);
        APIModel LoginRider(string username, string password);
        APIModel GetAllRequestDelivery(int status);
        APIModel GetAllRiders();        
        APIModel StartJob(StartJobBody body);        
    }
}
