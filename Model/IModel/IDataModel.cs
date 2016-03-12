using System;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 数据类型模型接口
    /// </summary>
    public interface IDataModel : IModel
    {
        /// <summary>
        /// 所属域ID
        /// </summary>
        Guid DomainId { get; set; }

        /// <summary>
        /// 所属域
        /// </summary>
        Domain Domain { get; set; }
    }
}