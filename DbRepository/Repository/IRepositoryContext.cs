using SHWDTech.Platform.Model.IModel;

namespace DbRepository.Repository
{
    /// <summary>
    /// 数据仓库上下文信息接口
    /// </summary>
    public interface IRepositoryContext
    {
        /// <summary>
        /// 当前操作用户
        /// </summary>
        IWdUser CurrentUser { get; set; }

        /// <summary>
        /// 当前操作域
        /// </summary>
        IDomain CurrentDomain { get; set; }
    }
}
