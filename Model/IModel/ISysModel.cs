﻿using System;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 系统模型接口
    /// </summary>
    public interface ISysModel : IModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// 字段创建时间
        /// </summary>
        DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 字段创建人ID
        /// </summary>
        Guid CreateUserId { get; set; }

        /// <summary>
        /// 字段最后修改时间
        /// </summary>
        DateTime? LastUpdateDateTime { get; set; }

        /// <summary>
        /// 字段最后修改人ID
        /// </summary>
        Guid? LastUpdateUserId { get; set; }

        /// <summary>
        /// 字段是否删除
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// 字段是否启用
        /// </summary>
        bool IsEnabled { get; set; }
    }
}