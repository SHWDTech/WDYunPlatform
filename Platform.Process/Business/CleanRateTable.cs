namespace Platform.Process.Business
{
    /// <summary>
    /// 清洁度视图
    /// </summary>
    public class CleanRateTable
    {
        public string DistrictName { get; set; }

        public string ProjectName { get; set; }

        public string DeviceName { get; set; }

        /// <summary>
        /// 失效总数
        /// </summary>
        public int Failed { get; set; }

        /// <summary>
        /// 较差总数
        /// </summary>
        public int Worse { get; set; }

        /// <summary>
        /// 合格总数
        /// </summary>
        public int Qualified { get; set; }

        /// <summary>
        /// 良好总数
        /// </summary>
        public int Good { get; set; }
    }
}
