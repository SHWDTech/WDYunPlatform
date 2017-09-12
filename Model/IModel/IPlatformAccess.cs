using System;

namespace SHWDTech.Platform.Model.IModel
{
    public interface IPlatformAccess : ISysDomainModel
    {
        /// <summary>
        /// 接入的平台名称
        /// </summary>
        string PlatformName { get; set; }

        /// <summary>
        /// 接入的设备ID
        /// </summary>
        Guid TargetGuid { get; set; }

        /// <summary>
        /// 接入时间
        /// </summary>
        DateTime AccessTime { get; set; }
    }
}
