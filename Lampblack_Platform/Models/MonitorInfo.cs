// ReSharper disable All

using System.Collections.Generic;

namespace Lampblack_Platform.Models
{
    public class MonitorInfos
    {
        public string result { get; set; } = "failed";

        public List<MonitorInfo> data { get; set; } = new List<MonitorInfo>();
    }

    public class MonitorInfo
    {
        /// <summary>
        /// 企业编码
        /// </summary>
        public string entp_id { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        public string entp_nam { get; set; }

        /// <summary>
        /// 浓度入
        /// </summary>
        public double entp_ndr { get; set; }

        /// <summary>
        /// 浓度出
        /// </summary>
        public double entp_ndc { get; set; }

        /// <summary>
        /// 清洁率
        /// </summary>
        public int entp_qjl { get; set; }

        /// <summary>
        /// 净化器名称
        /// </summary>
        public string entp_jhqmc { get; set; }

        /// <summary>
        /// 净化器开关状态
        /// </summary>
        public int entp_jhqkg { get; set; }

        /// <summary>
        /// 风机名称
        /// </summary>
        public string entp_fjmc { get; set; }

        /// <summary>
        /// 风机开关状态
        /// </summary>
        public int entp_fjkg { get; set; }

        /// <summary>
        /// 监测时间
        /// </summary>
        public string entp_jcsj { get; set; }

        /// <summary>
        /// 企业地址
        /// </summary>
        public string entp_adr { get; set; }
    }
}