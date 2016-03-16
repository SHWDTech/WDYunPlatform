using SHWDTech.Platform.Model.Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 协议接口
    /// </summary>
    public interface IProtocol : ISysModel
    {
        /// <summary>
        /// 协议应用领域ID
        /// </summary>
        Guid FieldId { get; set; }

        /// <summary>
        /// 协议应用领域
        /// </summary>
        SysDictionary Field { get; set; }

        /// <summary>
        /// 协议应用子领域ID
        /// </summary>
        Guid SubFieldId { get; set; }

        /// <summary>
        /// 协议应用子领域
        /// </summary>
        SysDictionary SubField { get; set; }

        /// <summary>
        /// 自定义信息
        /// </summary>
        string CustomerInfo { get; set; }

        /// <summary>
        /// 协议版本号
        /// </summary>
        string Version { get; set; }

        /// <summary>
        /// 协议的协议结构
        /// </summary>
        ICollection<ProtocolStructure> ProtocolStructures { get; set; }

        /// <summary>
        /// 协议发布时间
        /// </summary>
        DateTime ReleaseDateTime { get; set; }
    }
}