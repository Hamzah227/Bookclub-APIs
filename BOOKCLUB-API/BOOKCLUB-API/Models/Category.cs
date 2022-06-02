using System;
using System.Collections.Generic;

#nullable disable

namespace BOOKCLUB_API.Models
{
    public partial class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}
