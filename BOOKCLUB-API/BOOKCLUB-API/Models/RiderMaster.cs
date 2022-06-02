using System;
using System.Collections.Generic;

#nullable disable

namespace BOOKCLUB_API.Models
{
    public partial class RiderMaster
    {
        public long Id { get; set; }
        public long RequestDeliveryId { get; set; }
        public long? RiderId { get; set; }
        public long? ReqId { get; set; }
        public string RiderLocation { get; set; }
        public decimal? RiderLongitude { get; set; }
        public decimal? RiderLatitude { get; set; }
    }
}
