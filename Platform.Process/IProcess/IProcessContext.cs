using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Process.IProcess
{
    public interface IProcessContext
    {
        /// <summary>
        /// 当前操作用户
        /// </summary>
        IUser CurrentUser { get; set; }

        /// <summary>
        /// 当前操作域
        /// </summary>
        ISysDomain CurrentDomain { get; set; }
    }
}
