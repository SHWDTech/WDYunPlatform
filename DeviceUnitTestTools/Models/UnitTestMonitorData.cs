using System;
using System.ComponentModel;
// ReSharper disable LocalizableElement

namespace DeviceUnitTestTools.Models
{
    public class UnitTestMonitorData
    {
        [DisplayName("更新时间")]
        public DateTime UpdateTime { get; set; }

        [DisplayName("颗粒物")]
        public double Tp { get; set; }

        [DisplayName("噪音")]
        public double Db { get; set; }

        [DisplayName("温度")]
        public double Temperature { get; set; }

        [DisplayName("湿度")]
        public double Humidity { get; set; }

        [DisplayName("风速")]
        public double WindSpeed { get; set; }

        [DisplayName("风向")]
        public int WindDirection { get; set; }
    }
}
