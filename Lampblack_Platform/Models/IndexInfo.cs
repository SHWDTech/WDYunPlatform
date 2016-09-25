using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace Lampblack_Platform.Models
{
    public class IndexInfo
    {
        public string result { get; set; }

        public List<Index> data { get; set; } = new List<Index>();
    }

    public class Index
    {
        public string EQUP_ID { get; set; }

        public string RMON_TIME { get; set; }

        public string EQUP_VAL { get; set; }
    }
}