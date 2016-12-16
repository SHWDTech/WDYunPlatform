using System;
using System.Collections.Generic;

namespace SHWDTech.Platform.ProtocolService.DataBase
{
    public interface IProtocol
    {
        /// <summary>
        /// 协议名称
        /// </summary>
        string ProtocolName { get; set; }

        /// <summary>
        /// 协议处理模块
        /// </summary>
        string ProtocolModule { get; set; }

        /// <summary>
        /// 协议自定义段
        /// </summary>
        string CustomerInfo { get; set; }

        /// <summary>
        /// 协议版本号
        /// </summary>
        string Version { get; set; }

        /// <summary>
        /// 协议头
        /// </summary>
        byte[] Head { get; set; }

        /// <summary>
        /// 协议尾
        /// </summary>
        byte[] Tail { get; set; }

        /// <summary>
        /// 协议包含的机构
        /// </summary>
        ICollection<ProtocolStructure> ProtocolStructures { get; set; }

        /// <summary>
        /// 协议包含的指令
        /// </summary>
        ICollection<ProtocolCommand> ProtocolCommands { get; set; }

        /// <summary>
        /// 包含此协议的固件
        /// </summary>
        ICollection<Firmware> Firmwares { get; set; }

        /// <summary>
        /// 协议发布时间
        /// </summary>
        DateTime ReleaseDateTime { get; set; }

        /// <summary>
        /// 校验类型
        /// </summary>
        string CheckType { get; set; }
    }
}
