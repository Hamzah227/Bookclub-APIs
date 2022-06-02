using System;
using System.Collections.Generic;
using BOOKCLUB_API.Models;

namespace BOOKCLUB_API.WrapperClasses
{
    public class RequestDeliveryWrapper
    {
        public long ID { get; set; }
        public long RequestId { get; set; }
        public long BidId { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string? Status { get; set; } = string.Empty;
        public List<RequestDeliveryAttribute> deliveryAttribs { get; set; }
    }
}
