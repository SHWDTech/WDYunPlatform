using System;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 带域的系统模型接口
    /// </summary>
    public interface ISysDomainModel : ISysModel
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