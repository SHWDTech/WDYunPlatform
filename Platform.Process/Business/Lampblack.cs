using Platform.Process.Enums;
using SHWDTech.Platform.Model.Business;

namespace Platform.Process.Business
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
        public static string GetCleanessRate(double? current, CleanessRate rate)
        {
            if (current == null)
            {
                return CleanessRateResult.NoData;
            }
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

        public static int GetCleanessNumRate(double? current, CleanessRate rate)
        {
            if (current == null)
            {
                return 3;
            }
            if (current < rate.Fail)
            {
                return 2;
            }
            if (current < rate.Worse)
            {
                return 2;
            }
            return 1;
        }
    }
}
