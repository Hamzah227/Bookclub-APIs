using System;
using System.Collections.Generic;

#nullable disable

namespace BOOKCLUB_API.Models
{
    public partial class RequestAttribute
    {
        public long Id { get; set; }
        public string RequestId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
