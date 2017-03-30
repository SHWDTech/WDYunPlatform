// ReSharper disable InconsistentNaming

using System;

namespace Lampblack_Platform.Models.BootstrapTable
{
    public class BootstrapTablePostParams
    {
        public int limit { get; set; }

        public int offset { get; set; }

        public string order { get; set; }

        public string search { get; set; }

        public string sort { get; set; }
    }

    public class HistoryDataTable : BootstrapTablePostParams
    {
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}