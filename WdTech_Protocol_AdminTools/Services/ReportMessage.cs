using WdTech_Protocol_AdminTools.Enums;

namespace WdTech_Protocol_AdminTools.Services
{
    /// <summary>
    /// 报告消息
    /// </summary>
    public class ReportMessage
    {
        /// <summary>
        /// 消息颜色
        /// </summary>
        public string MessageColor
        {
            get
            {
                switch (_messageType)
                {
                    case ReportMessageType.Info:
                        return ReportMessageColor.Info;
                    case ReportMessageType.Error:
                        return ReportMessageColor.Error;
                    case ReportMessageType.Warning:
                        return ReportMessageColor.Warning;
                    case ReportMessageType.Fatal:
                        return ReportMessageColor.Fatal;
                    default:
                        return ReportMessageColor.Info;
                }
            }
        }

        /// <summary>
        /// 消息类型
        /// </summary>
        private readonly ReportMessageType _messageType;

        /// <summary>
        /// 报告消息
        /// </summary>
        public string Message { get; }

        public ReportMessage(ReportMessageType type, string message)
        {
            _messageType = type;
            Message = message;
        }
    }
}
