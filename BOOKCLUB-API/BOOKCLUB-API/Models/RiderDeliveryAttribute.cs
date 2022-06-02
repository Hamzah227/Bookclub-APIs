using System;
using System.Collections.Generic;

#nullable disable

namespace BOOKCLUB_API.Models
{
    public partial class RiderDeliveryAttribute
    {
        public long Id { get; set; }
        public long RiderMasterId { get; set; }
        public string Destination { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int? Priority { get; set; }
    }
}
