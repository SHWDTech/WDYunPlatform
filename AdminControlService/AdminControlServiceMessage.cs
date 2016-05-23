using System.Collections.Generic;

namespace SHWDTech.Platform.AdminControlService
{
    /// <summary>
    /// 管理工具控制服务消息
    /// </summary>
    public class AdminControlServiceMessage
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MessageType { get; set; }

        /// <summary>
        /// 消息参数列表
        /// </summary>
        public List<object> MessageParams { get; set; }

        /// <summary>
        /// 消息指令
        /// </summary>
        public string MessageCommand { get; set; }
    }
}
