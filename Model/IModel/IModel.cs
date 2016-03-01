using System;
using SHWDTech.Platform.Model.Enum;

namespace SHWDTech.Platform.Model.IModel
{
    public interface IModel
    {
        /// <summary>
        /// 数据唯一标识ID
        /// </summary>
        Guid Guid { get; set; }

        /// <summary>
        /// 对象状态
        /// </summary>
        ModelState ModelState { get; set; }

        /// <summary>
        /// 是否是新创建的对象
        /// </summary>
        bool IsNew { get; }
    }
}
