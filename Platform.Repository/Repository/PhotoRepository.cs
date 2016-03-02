using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 图片数据仓库
    /// </summary>
    internal class PhotoRepository : SysDomainRepository<IPhoto>, IPhotoRepository
    {
    }
}
