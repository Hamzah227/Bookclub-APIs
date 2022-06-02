using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BOOKCLUB_API.Models;

namespace BOOKCLUB_API.WrapperClasses
{
    public class BidsWrapperClass
    {
        public long Id { get; set; }
        public string Uid { get; set; }
        public string? RequestId { get; set; }
        public string Status { get; set; }
        public long? StatusProgress { get; set; }
        public string Comments { get; set; }
        public List<BidAttributeBook> Books { get; set; }
        public List<BidAttribute> bidAttributes = new List<BidAttribute>();
    }
} 
