using System;
using System.Collections.Generic;

#nullable disable

namespace BOOKCLUB_API.Models
{
    public partial class WorkOrder
    {
        public long Id { get; set; }
        public long BidId { get; set; }
        public long ReqId { get; set; }
        public string Status { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
