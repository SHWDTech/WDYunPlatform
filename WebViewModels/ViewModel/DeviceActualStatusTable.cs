using System;

namespace WebViewModels.ViewModel
{
    public class DeviceActualStatusTable
    {
        public Guid ProjectGuid { get; set; }

        public string DistrictName { get; set; }

        public string ProjectName { get; set; }

        public string DeviceName { get; set; }

        public string Channel { get; set; }

        public string CleanRate { get; set; } = "无数据";

        public bool CleanerStatus { get; set; } = false;

        public string CleanerCurrent { get; set; } = "N/A";

        public bool FanStatus { get; set; } = false;

        public string RecordDateTime { get; set; } = "N/A";
    }
}
