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
        public Guid Hotel { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }

    public class AcutalDataTable : BootstrapTablePostParams
    {
        public Guid Area { get; set; }

        public Guid Street { get; set; }

        public Guid Address { get; set; }

        public string Name { get; set; }
    }

    public class RunngingDataTable : BootstrapTablePostParams
    {
        public Guid Area { get; set; }

        public Guid Street { get; set; }

        public Guid Address { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Name { get; set; }
    }

    public class CleanRateDataTable : BootstrapTablePostParams
    {
        public Guid Area { get; set; }

        public Guid Street { get; set; }

        public Guid Address { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Name { get; set; }
    }

    public class LinkageRateTable : BootstrapTablePostParams
    {
        public Guid Area { get; set; }

        public Guid Street { get; set; }

        public Guid Address { get; set; }

        public DateTime QueryDateTime { get; set; }

        public string Name { get; set; }
    }

    public class CateringEnterpriseTable : BootstrapTablePostParams
    {
        public Guid Area { get; set; }

        public Guid Street { get; set; }

        public Guid Address { get; set; }

        public string QueryName { get; set; }
    }

    public class HotelTable : BootstrapTablePostParams
    {
        public Guid Area { get; set; }

        public Guid Street { get; set; }

        public Guid Address { get; set; }

        public string QueryName { get; set; }
    }

    public class DeviceTable : BootstrapTablePostParams
    {
        public Guid Area { get; set; }

        public Guid Street { get; set; }

        public Guid Address { get; set; }

        public string QueryName { get; set; }
    }
}