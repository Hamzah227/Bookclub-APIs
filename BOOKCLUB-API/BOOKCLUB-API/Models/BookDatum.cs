using System;
using System.Collections.Generic;

#nullable disable

namespace BOOKCLUB_API.Models
{
    public partial class BookDatum
    {
        public long Id { get; set; }
        public string ReqId { get; set; }
        public int? TypeId { get; set; }
        public string BookTitle { get; set; }
        public string Isbn { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public string Categories { get; set; }
        public string Status { get; set; }
    }
}
