using System;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 任务处理接口
    /// </summary>
    public interface ICommandTaskProcess
    {
        /// <summary>
        /// 获取指定ID的任务信息
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        CommandTask GetTaskByGuid(Guid taskId);

        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="commandTask"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool UPdateTaskStatus(CommandTask commandTask, TaskStatus status);

        /// <summary>
        /// 更新执行任务状态
        /// </summary>
        /// <param name="commandTask"></param>
        /// <param name="exceteStatus"></param>
        /// <returns></returns>
        bool UpdateExecuteStatus(CommandTask commandTask, TaskExceteStatus exceteStatus);
    }
}
