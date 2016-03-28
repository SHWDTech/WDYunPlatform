namespace Web_Cloud_Platform.Models
{
    /// <summary>
    /// 公司信息接口
    /// </summary>
    public interface ICompanyInfomation
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        string CompanyName { get; set; }

        /// <summary>
        /// 平台名称
        /// </summary>
        string PlatformName { get; set; }
    }
}