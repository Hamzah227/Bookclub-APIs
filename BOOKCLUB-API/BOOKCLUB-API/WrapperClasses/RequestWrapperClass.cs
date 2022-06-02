using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BOOKCLUB_API.Models;

namespace BOOKCLUB_API.WrapperClasses
{
    public class RequestWrapperClass
    {
        public long ReqId { get; set; }
        public string Uid { get; set; }
        public bool? IsArchived { get; set; }
        public string TypeId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? ExpiresOn { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public string Status { get; set; }
        public bool? IsActive { get; set; }
        public List<BidsWrapperClass> bids = new List<BidsWrapperClass>();
        public List<RequestAttribute> requestAttributes = new List<RequestAttribute>();
        public List<Books> BooksForSell = new List<Books>();
        public List<Books> BooksForExchange = new List<Books>();

    }
}
