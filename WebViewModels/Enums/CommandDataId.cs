using System;

namespace WebViewModels.Enums
{
    public static class CommandDataId
    {
        /// <summary>
        /// 净化器开关
        /// </summary>
        public static Guid CleanerSwitch { get; set; }

        /// <summary>
        /// 净化器电流
        /// </summary>
        public static Guid CleanerCurrent { get; set; }

        /// <summary>
        /// 风机开关
        /// </summary>
        public static Guid FanSwitch { get; set; }

        /// <summary>
        /// 风机电流
        /// </summary>
        public static Guid FanCurrent { get; set; }

        /// <summary>
        /// 进口油烟浓度
        /// </summary>
        public static Guid LampblackInCon { get; set; }

        /// <summary>
        /// 出口油烟浓度
        /// </summary>
        public static Guid LampblackOutCon { get; set; }
    }
}
