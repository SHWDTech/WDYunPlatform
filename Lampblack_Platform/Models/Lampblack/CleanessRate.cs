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
        public readonly int Fail;

        /// <summary>
        /// 较差
        /// </summary>
        public readonly int Worse;

        /// <summary>
        /// 合格
        /// </summary>
        public readonly int Qualified;

        /// <summary>
        /// 良好
        /// </summary>
        public readonly int Good;

        private CleanessRate()
        {
            
        }

        public CleanessRate(int[] rate) : this()
        {
            Fail = rate[0];
            Worse = rate[1];
            Qualified = rate[2];
            Good = rate[3];
        }
    }
}