using System;
using System.Collections.Generic;

#nullable disable

namespace BOOKCLUB_API.Models
{
    public partial class EnrouteProcess
    {
        public long Id { get; set; }
        public long? RiderId { get; set; }
        public long ReqId { get; set; }
        public long? DeliveryId { get; set; }
        public DateTime? Timestamp { get; set; }
        public string Status { get; set; }
    }
}
