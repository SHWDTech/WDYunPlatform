using System;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    internal interface ISysModel
    {
        /// <summary>
        /// 字段创建时间
        /// </summary>
        DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 字段创建人
        /// </summary>
        User CreateUser { get; set; }

        /// <summary>
        /// 字段最后一次修改时间
        /// </summary>
        DateTime LastUpdateDateTime { get; set; }

        /// <summary>
        /// 字段最后一次修改人
        /// </summary>
        User LastUpdateUser { get; set; }

        /// <summary>
        /// 字段是否删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
