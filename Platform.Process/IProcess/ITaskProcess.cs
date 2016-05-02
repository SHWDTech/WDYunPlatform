using System;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 任务处理接口
    /// </summary>
    public interface ITaskProcess
    {
        /// <summary>
        /// 获取指定ID的任务信息
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        Task GetTaskByGuid(Guid taskId);

        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="task"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool UpdateExecuteStatus(Task task, TaskExceteStatus status);
    }
}
