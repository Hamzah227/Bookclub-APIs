using System;
using System.Collections.Generic;

#nullable disable

namespace BOOKCLUB_API.Models
{
    public partial class Destination
    {
        public long Id { get; set; }
        public string Destination1 { get; set; }
        public bool? HasReached { get; set; }
    }
}
