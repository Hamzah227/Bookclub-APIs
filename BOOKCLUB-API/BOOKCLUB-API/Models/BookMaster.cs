using System;
using System.Collections.Generic;

#nullable disable

namespace BOOKCLUB_API.Models
{
    public partial class BookMaster
    {
        public long? Id { get; set; }
        public long? RequestId { get; set; }
        public string BookName { get; set; }
        public string BookIsbn { get; set; }
        public string BookCategory { get; set; }
        public string RequestType { get; set; }
        public string RequestStatus { get; set; }
    }
}
