using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace SHWDTech.Platform.StorageConstrains.Repository
{
    public interface IRepositoryBase
    {
        /// <summary>
        /// 是否自动提交
        /// </summary>
        bool AutoCommit { get; set; }

        /// <summary>
        /// 数据上下文
        /// </summary>
        DbContext DbContext { get; set; }

        /// <summary>
        /// 数据上下文相关数据库设置
        /// </summary>
        Database DataBase { get; }

        /// <summary>
        /// 数据上下文相关设置
        /// </summary>
        DbContextConfiguration Configuration { get; }
    }
}
