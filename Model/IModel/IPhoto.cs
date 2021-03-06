﻿using System;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 图片模型接口
    /// </summary>
    public interface IPhoto : ISysDomainModel
    {
        /// <summary>
        /// 照片所属设备ID
        /// </summary>
        Guid? DeviceId { get; set; }

        /// <summary>
        /// 照片所属设备
        /// </summary>
        Device Device { get; set; }

        /// <summary>
        /// 照片附加信息
        /// </summary>
        string PhotoTag { get; set; }

        /// <summary>
        /// 照片地址
        /// </summary>
        string PhotoUrl { get; set; }

        /// <summary>
        /// 照片类型ID
        /// </summary>
        Guid PhotoTypeId { get; set; }

        /// <summary>
        /// 照片类型
        /// </summary>
        SysDictionary PhotoType { get; set; }
    }
}