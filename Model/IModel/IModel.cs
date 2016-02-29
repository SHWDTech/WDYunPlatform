using System;

namespace SHWDTech.Platform.Model.IModel
{
    public interface IModel
    {
        /// <summary>
        /// 数据唯一标识ID
        /// </summary>
         Guid Guid { get; set; }
    }
}
