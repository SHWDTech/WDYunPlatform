using Lampblack_Platform.Enums;
using Lampblack_Platform.Models.Lampblack;

namespace Lampblack_Platform.Utility
{
    /// <summary>
    /// 油烟相关业务算法
    /// </summary>
    public static class Lampblack
    {
        /// <summary>
        /// 获取清洁度等级
        /// </summary>
        /// <param name="current"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static string GetCleanessRate(double current, CleanessRate rate)
        {
            if (current < rate.Fail)
            {
                return CleanessRateResult.Fail;
            }
            if (current < rate.Worse)
            {
                return CleanessRateResult.Worse;
            }
            return current < rate.Qualified ? CleanessRateResult.Qualified : CleanessRateResult.Good;
        }
    }
}
