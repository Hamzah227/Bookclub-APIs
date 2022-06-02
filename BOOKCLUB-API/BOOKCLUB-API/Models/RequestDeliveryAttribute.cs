using System;
using System.Collections.Generic;

#nullable disable

namespace BOOKCLUB_API.Models
{
    public partial class RequestDeliveryAttribute
    {
        public long Id { get; set; }
        public long? BidId { get; set; }
        public long? RequestDeliveryId { get; set; }
        public string PickupAddress { get; set; }
        public string DropOffAddress { get; set; }
        public double? PickupLong { get; set; }
        public double? PickupLat { get; set; }
        public double? DropOffLat { get; set; }
        public double? DropOffLong { get; set; }
        public string Status { get; set; }
    }
}
