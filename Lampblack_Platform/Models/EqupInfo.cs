using System.Collections.Generic;
// ReSharper disable All

namespace Lampblack_Platform.Models
{
    public class EqupInfo
    {
        public string result { get; set; } = "Falid";

        public List<Equp> data { get; set; } = new List<Equp>();
    }

    public class Equp
    {
        public string EQUP_ID { get; set; }

        public string EQUP_NAM { get; set; }

        public string CASE_ID { get; set; }

        public string EQUP_TYP { get; set; }

        public string EQUP_MOD { get; set; }

        public string EQUP_LIM { get; set; }
    }
}