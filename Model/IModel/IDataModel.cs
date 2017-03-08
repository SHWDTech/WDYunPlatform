using System;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 数据类型模型接口
    /// </summary>
    public interface IDataModel : IModel
    {
        /// <summary>
        /// 数据表主键
        /// </summary>
        long Id { get; set; }

        /// <summary>
        /// 所属域ID
        /// </summary>
        Guid DomainId { get; set; }
    }
}