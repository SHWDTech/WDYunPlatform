using SHWDTech.Platform.Model.Model;

namespace Lampblack_Platform.Models.Lampblack
{
    /// <summary>
    /// 清洁度
    /// </summary>
    public class CleanessRate
    {
        /// <summary>
        /// 失效
        /// </summary>
        public int Fail { get; set; }

        /// <summary>
        /// 较差
        /// </summary>
        public int Worse { get; set; }

        /// <summary>
        /// 合格
        /// </summary>
        public int Qualified { get; set; }

        /// <summary>
        /// 良好
        /// </summary>
        public int Good { get; set; }

        private CleanessRate()
        {
            
        }

        public CleanessRate(LampblackDeviceModel rate) : this()
        {
            Fail = rate.Fail;

            Worse = rate.Worse;

            Qualified = rate.Qualified;

            Good = rate.Good;
        }
    }
}