using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BOOKCLUB_API.Models
{
    public class APIModel
    {
        public bool success { get; set; }
        public string message { get; set; }
        public dynamic data { get; set; }
        public string status { get; set; }
        public string timeStamp { get; set; }

    }
}
