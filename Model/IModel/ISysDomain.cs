namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 带有域的模型接口
    /// </summary>
    public interface ISysDomain : IModel
    {
        /// <summary>
        /// 域名称
        /// </summary>
        string DomainName { get; set; }

        /// <summary>
        /// 域类型
        /// </summary>
        string DomianType { get; set; }
    }
}
