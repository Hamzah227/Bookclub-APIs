using System;
using System.Collections.Generic;

#nullable disable

namespace BOOKCLUB_API.Models
{
    public partial class Request
    {
        public long ReqId { get; set; }
        public string Uid { get; set; }
        public bool? IsArchived { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? ExpiresOn { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public string TypeId { get; set; }
        public string Status { get; set; }
        public bool? IsActive { get; set; }
    }
}
