using SHWDTech.Platform.Model.Model;
using System;
using System.Collections.Generic;
using SHWDTech.Platform.Model.Enums;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 任务模型接口
    /// </summary>
    public interface ITask : ISysDomainModel
    {
        /// <summary>
        /// 任务所属设备ID
        /// </summary>
        Guid TaskDeviceId { get; set; }

        /// <summary>
        /// 任务所属设备
        /// </summary>
        Device TaskDevice { get; set; }

        /// <summary>
        /// 任务编码
        /// </summary>
        string TaskCode { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        int TaskType { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        TaskStatus TaskStatus { get; set; }

        /// <summary>
        /// 任务执行状态
        /// </summary>
        TaskExceteStatus ExecuteStatus { get; set; }

        /// <summary>
        /// 任务包含协议
        /// </summary>
        ICollection<ProtocolData> TaskProtocols { get; set; }

        /// <summary>
        /// 任务生成时间
        /// </summary>
        DateTime SetUpDateTime { get; set; }
    }
}