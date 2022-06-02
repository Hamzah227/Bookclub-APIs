using System.Collections.Generic;

namespace BOOKCLUB_API.Models
{
    public partial class DistanceMatrix
    {
        public List<string> destination_addresses { get; set; }
        public List<string> origin_addresses { get; set; }
        public List<Row> Rows { get; set; }
        public string Status { get; set; }
    }

    public partial class Row
    {
        public List<Element> Elements { get; set; }
    }

    public partial class Element
    {
        public Distance Distance { get; set; }
        public Distance Duration { get; set; }
        public string Status { get; set; }
    }

    public partial class Distance
    {
        public string Text { get; set; }
        public long Value { get; set; }
    }
}
