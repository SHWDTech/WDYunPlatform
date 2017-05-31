namespace Platform.Process.Enums
{
    /// <summary>
    /// 清洁度计算结果
    /// </summary>
    public static class CleanessRateResult
    {
        /// <summary>
        /// 无数据
        /// </summary>
        public const string NoData = "电源未接通";

        /// <summary>
        /// 失效
        /// </summary>
        public const string Fail = "失效";

        /// <summary>
        /// 较差
        /// </summary>
        public const string Worse = "较差";

        /// <summary>
        /// 合格
        /// </summary>
        public const string Qualified = "合格";

        /// <summary>
        /// 良好
        /// </summary>
        public const string Good = "良好";
    }
}