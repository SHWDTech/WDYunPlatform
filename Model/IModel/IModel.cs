﻿using System;
using SHWDTech.Platform.Model.Enums;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 模型基础接口
    /// </summary>
    public interface IModel
    {
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