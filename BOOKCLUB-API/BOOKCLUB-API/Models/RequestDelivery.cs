using System;
using System.Collections.Generic;

#nullable disable

namespace BOOKCLUB_API.Models
{
    public partial class RequestDelivery
    {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public DateTime? Timestamp { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Status { get; set; }
    }
}
