using System;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 运行时间处理程序接口
    /// </summary>
    public interface IRunningTimeProcess
    {
        /// <summary>
        /// 最后记录时间
        /// </summary>
        /// <param name="hotelGuid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        DateTime LastRecordDateTime(Guid hotelGuid, RunningTimeType type);

        /// <summary>
        /// 存储运行时间数据
        /// </summary>
        /// <param name="runningTime"></param>
        void StoreRunningTime(RunningTime runningTime);
    }
}
