using System;
using SHWDTech.Platform.Model.Enums;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 设备运行时间模型接口
    /// </summary>
    public interface IRunningTime : IDataModel
    {
        /// <summary>
        /// 运行时间数值
        /// </summary>
        long RunningTimeTicks { get; set; }

        /// <summary>
        /// 运行时间
        /// </summary>
        TimeSpan RunningTimeSpan { get; set; }

        /// <summary>
        /// 运行时间类型
        /// </summary>
        RunningTimeType Type { get; set; }

        /// <summary>
        /// 运行时间关联时间ID
        /// </summary>
        long ProjectIdentity { get; set; }

        /// <summary>
        /// 运行时间关联设备ID
        /// </summary>
        long DeviceIdentity { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        DateTime UpdateTime { get; set; }
    }
}
