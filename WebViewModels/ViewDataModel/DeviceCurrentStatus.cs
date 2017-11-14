using System;

namespace WebViewModels.ViewDataModel
{
    public class DeviceCurrentStatus
    {
        public double FanCurrent { get; set; }

        public double CleanerCurrent { get; set; }

        public string CleanerStatus => CleanerSwitch ? "开启" : "关闭";

        public bool CleanerSwitch { get; set; }

        public string FanStatus => FanSwitch ? "开启" : "关闭";

        public bool FanSwitch { get; set; }

        public double LampblackIn { get; set; }

        public double LampblackOut { get; set; }

        public string CleanRate { get; set; }

        public string FanRunTime => TimeSpan.FromTicks(FanRunTimeTicks).ToString("h'小时 'm'分钟 's'秒'");

        public long FanRunTimeTicks { get; set; }

        public string CleanerRunTime => TimeSpan.FromTicks(CleanerRunTimeTicks).ToString("h'小时 'm'分钟 's'秒'");

        public long CleanerRunTimeTicks { get; set; }

        public long UpdateTime { get; set; }
    }
}
