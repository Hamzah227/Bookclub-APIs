using System;
using System.Collections.Generic;

#nullable disable

namespace BOOKCLUB_API.Models
{
    public partial class BidAttributeBook
    {
        public long Id { get; set; }
        public long BidId { get; set; }
        public string ImageUrl { get; set; }
        public bool? Selected { get; set; }
        public string Isbn { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
    }
}
