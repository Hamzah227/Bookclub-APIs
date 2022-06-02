using System;
using System.Collections.Generic;

#nullable disable

namespace BOOKCLUB_API.Models
{
    public partial class WorkOrderAttribute
    {
        public long Id { get; set; }
        public long WorkOrderId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? InProgress { get; set; }
    }
}
