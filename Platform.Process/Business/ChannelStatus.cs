namespace Platform.Process.Business
{
    /// <summary>
    /// 通道状态
    /// </summary>
    public class ChannelStatus
    {
        /// <summary>
        /// 清洁度
        /// </summary>
        public string CleanRate { get; set; } = "/Resources/Images/Site/CleanRate/N_3232.png";

        /// <summary>
        /// 净化器状态
        /// </summary>
        public string CleanerSwitch { get; set; } = "/Resources/Images/Site/CleanRate/WARNING.png";

        /// <summary>
        /// 风机状态
        /// </summary>
        public string FanSwitch { get; set; } = "/Resources/Images/Site/CleanRate/WARNING.png";

        /// <summary>
        /// 出烟浓度
        /// </summary>
        public string LampblackOut { get; set; } = "N/A";

        /// <summary>
        /// 净化器电流
        /// </summary>
        public string CleanerCurrent { get; set; } = "N/A";

        /// <summary>
        /// 风机电流
        /// </summary>
        public string FanCurrent { get; set; } = "N/A";

        /// <summary>
        /// 监测时间
        /// </summary>
        public string UpdateTime { get; set; } = "N/A";
    }
}
