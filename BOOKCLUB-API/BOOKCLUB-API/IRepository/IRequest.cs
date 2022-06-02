using BOOKCLUB_API.Models;
using BOOKCLUB_API.WrapperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BOOKCLUB_API.IRepository
{
   public interface IRequest
    {
        APIModel SaveRequest(RequestWrapperClass requestWrapperClass);
        APIModel SaveBid(BidsWrapperClass bidsWrapperClass);
        APIModel GetAllRequest(string UID, bool myAds);
        APIModel GetUserRequest(string UID);
        APIModel ApproveBid(long BidId);
        APIModel GetBids(string Uid);
        APIModel RejectBid(long BidId);
        APIModel GetAllRequestForAdmin();
    }
}
