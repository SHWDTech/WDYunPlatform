using System;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding.Enums;
using SHWDTech.Platform.Utility;
using System.Collections.Generic;
using System.Linq;
using SHWDTech.Platform.ProtocolCoding.Generics;

namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    /// <summary>
    /// 协议编解码器
    /// </summary>
    public class ProtocolEncoder
    {
        /// <summary>
        /// 设备包含协议
        /// </summary>
        private readonly List<Protocol> _deviceProtocols = new List<Protocol>();

        /// <summary>
        /// 协议对应的解码器类实例
        /// </summary>
        private static readonly Dictionary<string, ICommandCoder<Type>> CommandCoders = new Dictionary<string, ICommandCoder<Type>>();

        /// <summary>
        /// 解码包对应设备
        /// </summary>
        private readonly Device _device;

        public ProtocolEncoder(IDevice device)
        {
            _device = (Device)device;

            foreach (var firmware in device.FirmwareSet.Firmwares)
            {
                var protocols =
                    ProtocolInfoManager.AllProtocols.Where(obj => obj.Firmwares.Any(firm => firm.Id == firmware.Id))
                        .ToList();
                foreach (var protocol in protocols)
                {
                    if (!_deviceProtocols.Contains(protocol))
                    {
                        _deviceProtocols.Add(protocol);
                    }
                }
            }
        }

        /// <summary>
        /// 将字节流解码成协议包
        /// </summary>
        /// <returns></returns>
        public IProtocolPackage Decode(byte[] bufferBytes)
        {
            var package = Decode(bufferBytes, _deviceProtocols);
            package.Device = _device;

            return package;
        }

        /// <summary>
        /// 对指定的指令进行编码
        /// </summary>
        /// <param name="command"></param>
        /// <param name="paramBytes"></param>
        /// <returns></returns>
        public byte[] Encode(ProtocolCommand command, Dictionary<string, byte[]> paramBytes = null) 
            => EncodeProtocol(command, paramBytes);

        /// <summary>
        /// 根据指定的协议集解码协议
        /// </summary>
        /// <param name="bufferBytes"></param>
        /// <param name="protocols"></param>
        /// <returns></returns>
        public static IProtocolPackage Decode(byte[] bufferBytes, List<Protocol> protocols)
        {
            var matchedProtocol = DetectProtocol(bufferBytes, protocols);

            if(matchedProtocol == null) return new ProtocolPackage { Status = PackageStatus.InvalidHead };

            var commandCoder = GetCommandCoder(matchedProtocol.ProtocolModule);

            return commandCoder.DecodeProtocol(bufferBytes, matchedProtocol);
        }

        /// <summary>
        /// 监测字节流所属协议
        /// </summary>
        /// <param name="bufferBytes">缓存字节数组</param>
        /// <param name="protocols">准备匹配的协议列表</param>
        /// <returns></returns>
        public static Protocol DetectProtocol(byte[] bufferBytes, List<Protocol> protocols)
            => protocols.FirstOrDefault(obj => IsHeadMatched(bufferBytes, obj.Head));

        /// <summary>
        /// 协议帧头与字节流匹配
        /// </summary>
        /// <param name="bufferBytes">协议字节流</param>
        /// <param name="protocolHead">协议定义帧头</param>
        /// <returns>匹配返回TRUE，否则返回FALSE</returns>
        public static bool IsHeadMatched(byte[] bufferBytes, byte[] protocolHead)
        {
            if (bufferBytes.Length < protocolHead.Length)
            {
                return false;
            }

            return !protocolHead.Where((t, i) => bufferBytes[i] != t).Any();
        }

        /// <summary>
        /// 协议编码
        /// </summary>
        /// <param name="command">指定的指令</param>
        /// <param name="paramBytes"></param>
        /// <returns>协议字节流</returns>
        public static byte[] EncodeProtocol(IProtocolCommand command, Dictionary<string, byte[]> paramBytes = null)
            => UnityFactory.Resolve<ICommandCoder<Type>>(command.Protocol.ProtocolModule).EncodeCommand(command, paramBytes).GetBytes();

        private static ICommandCoder<Type> GetCommandCoder(string protocolName)
        {
            if (CommandCoders.ContainsKey(protocolName)) return CommandCoders[protocolName];
            var coder = UnityFactory.Resolve<ICommandCoder<Type>>(protocolName);
            CommandCoders.Add(protocolName, coder);

            return CommandCoders[protocolName];
        }
    }
}