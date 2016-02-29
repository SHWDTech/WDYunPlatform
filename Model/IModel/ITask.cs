using System;
using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    public interface ITask : ISysModel, IDomainModel
    {
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
        int TaskStatus { get; set; }

        /// <summary>
        /// 任务执行状态
        /// </summary>
        int ExecuteStatus { get; set; }

        /// <summary>
        /// 任务包含协议
        /// </summary>
        List<Protocol> TaskProtocols { get; set; } 

        /// <summary>
        /// 任务生成时间
        /// </summary>
        DateTime SetUpDateTime { get; set; }
    }
}
