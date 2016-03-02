﻿namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 带域的系统模型接口
    /// </summary>
    public interface ISysDomainModel : ISysModel
    {
        /// <summary>
        /// 所属域
        /// </summary>
        IDomain Domain { get; set; }
    }
}