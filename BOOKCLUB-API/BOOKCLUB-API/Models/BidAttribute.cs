﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BOOKCLUB_API.Models
{
    public partial class BidAttribute
    {
        public long Id { get; set; }
        public string BidId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
