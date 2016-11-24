// ReSharper disable All

using System.Collections.Generic;

namespace Lampblack_Platform.Models
{
    public class EnterpriseInfo
    {
        public string result { get; set; } = "Faild";

        public List<Enterprise> data { get; set; } = new List<Enterprise>();
    }

    public class Enterprise
    {
        public string QYBM { get; set; }

        public string QYMC { get; set; }

        public string QYDZ { get; set; }

        public string PER { get; set; }

        public string TEL { get; set; }

        public string QYSTREET { get; set; }

        public string XPOS { get; set; }

        public string YPOS { get; set; }

        public string CASE_ID { get; set; }

        public string CASE_NAM { get; set; }

        public string CASE_TEL { get; set; }

        public string CASE_IP { get; set; }
    }
}