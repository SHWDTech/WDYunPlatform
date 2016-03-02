using System;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 报警信息数据接口
    /// </summary>
    public interface IAlarm : IDataModel
    {
        /// <summary>
        /// 报警信息所属设备
        /// </summary>
        Device Device { get; set; }

        /// <summary>
        /// 报警信息值
        /// </summary>
        double AlarmValue { get; set; }

        /// <summary>
        /// 报警类别
        /// </summary>
        int AlarmType { get; set; }

        /// <summary>
        /// 数据更新时间
        /// </summary>
        DateTime UpdateTime { get; set; }
    }
}
