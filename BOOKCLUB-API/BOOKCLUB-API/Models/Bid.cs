using System;
using System.Collections.Generic;

#nullable disable

namespace BOOKCLUB_API.Models
{
    public partial class Bid
    {
        public long Id { get; set; }
        public string Uid { get; set; }
        public string RequestId { get; set; }
        public string Status { get; set; }
        public long? StatusProgress { get; set; }
        public string Comments { get; set; }
    }
}
