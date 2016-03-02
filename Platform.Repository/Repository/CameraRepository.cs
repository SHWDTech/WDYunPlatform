using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 摄像头数据仓库
    /// </summary>
    internal class CameraRepository : SysDomainRepository<ICamera>, ICameraRepository
    {
        /// <summary>
        /// 创建新的摄像头数据仓库实例
        /// </summary>
        protected CameraRepository()
        {
            
        }
    }
}
