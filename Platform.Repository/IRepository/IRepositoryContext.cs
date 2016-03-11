using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.IRepository
{
    /// <summary>
    /// 数据仓库上下文信息接口
    /// </summary>
    public interface IRepositoryContext
    {
        /// <summary>
        /// 当前操作用户
        /// </summary>
        WdUser CurrentUser { get; set; }

        /// <summary>
        /// 当前操作域
        /// </summary>
        Domain CurrentDomain { get; set; }
    }
}