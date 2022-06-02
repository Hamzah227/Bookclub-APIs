using System;
using System.Collections.Generic;

#nullable disable

namespace BOOKCLUB_API.Models
{
    public partial class SourceAndDestination
    {
        public long Id { get; set; }
        public long? ReqDeliveryId { get; set; }
        public long? RiderUserId { get; set; }
        public string Status { get; set; }
    }
}
