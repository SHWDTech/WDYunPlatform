using System;

namespace WebViewModels.ViewDataModel
{
    /// <summary>
    /// 综合对比
    /// </summary>
    public class GeneralCompasion
    {
        /// <summary>
        /// 区域ID
        /// </summary>
        public Guid AreaGuid { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 当前联动比
        /// </summary>
        public double CurrentLinkage { get; set; }

        /// <summary>
        /// 同比联动比
        /// </summary>
        public double LastLinkage { get; set; }

        /// <summary>
        /// 环比联动比
        /// </summary>
        public double LinkedLinkage { get; set; }
    }
}
