using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding.Command;
using SHWDTech.Platform.ProtocolCoding.Enums;
using SHWDTech.Platform.Utility;
using System.Collections.Generic;
using System.Linq;

namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    /// <summary>
    /// 协议编解码器
    /// </summary>
    public class ProtocolEncoding
    {
        /// <summary>
        /// 设备包含协议
        /// </summary>
        private readonly List<Protocol> _deviceProtocols = new List<Protocol>();

        public ProtocolEncoding(IDevice device)
        {
            foreach (var firmware in device.FirmwareSet.Firmwares)
            {
                _deviceProtocols.AddRange(firmware.Protocols);
            }
        }

        /// <summary>
        /// 将字节流解码成协议包
        /// </summary>
        /// <param name="bufferBytes"></param>
        /// <returns></returns>
        public IProtocolPackage Decode(byte[] bufferBytes)
            => Decode(bufferBytes, _deviceProtocols);

        /// <summary>
        /// 对指定的指令进行编码
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public byte[] Encode(ProtocolCommand command) => EncodeProtocol(command);

        /// <summary>
        /// 根据指定的协议集解码协议
        /// </summary>
        /// <param name="bufferBytes"></param>
        /// <param name="protocols"></param>
        /// <returns></returns>
        public static IProtocolPackage Decode(byte[] bufferBytes, List<Protocol> protocols)
        {
            var matchedProtocol = DetectProtocol(bufferBytes, protocols);

            return matchedProtocol == null
                ? new ProtocolPackage() { Status = PackageStatus.InvalidHead }
                : DecodeProtocol(bufferBytes, matchedProtocol);
        }

        /// <summary>
        /// 监测字节流所属协议
        /// </summary>
        /// <param name="bufferBytes">缓存字节数组</param>
        /// <param name="protocols">准备匹配的协议列表</param>
        /// <returns></returns>
        public static IProtocol DetectProtocol(byte[] bufferBytes, List<Protocol> protocols)
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
        /// <returns>协议字节流</returns>
        public static byte[] EncodeProtocol(IProtocolCommand command)
        {
            var package = UnityFactory.Resolve<ICommandCoding>(command.Protocol.ProtocolModule).EncodeCommand(command);

            var byteList = new List<byte>();
            var structures = command.Protocol.ProtocolStructures;

            for (var i = 0; i < structures.Count; i++)
            {
                var structure = structures.First(struc => struc.ComponentIndex == i);
                byteList.AddRange(package[structure.ComponentName].ComponentBytes);
            }

            return byteList.ToArray();
        }

        /// <summary>
        /// 协议解码
        /// </summary>
        /// <param name="bufferBytes">字节流</param>
        /// <param name="matchedProtocol">对应的协议</param>
        /// <returns>协议解析结果</returns>
        public static IProtocolPackage DecodeProtocol(byte[] bufferBytes, IProtocol matchedProtocol)
        {
            var package = new ProtocolPackage() {Protocol = matchedProtocol};

            var structures = matchedProtocol.ProtocolStructures.ToList();

            var commandCoder = UnityFactory.Resolve<ICommandCoding>(matchedProtocol.ProtocolModule);

            var currentIndex = 0;

            for (var i = 0; i < structures.Count; i++)
            {
                var structure = structures.First(obj => obj.ComponentIndex == i);

                if (currentIndex + structure.ComponentDataLength > bufferBytes.Length)
                {
                    package.Status = PackageStatus.NoEnoughBuffer;
                    return package;
                }

                var componentDataLength = structure.ComponentDataLength;

                if (structure.ComponentName == StructureNames.Data)
                {
                    commandCoder.DetectCommand(package, matchedProtocol);
                    componentDataLength = package.Command.CommandBytesLength;
                }

                var component = new PackageComponent
                {
                    ComponentName = structure.ComponentName,
                    DataType = structure.DataType,
                    ComponentIndex = structure.ComponentIndex,
                    ComponentBytes = bufferBytes.SubBytes(currentIndex, currentIndex + componentDataLength)
                };

                currentIndex += structure.ComponentDataLength;

                package[structure.ComponentName] = component;
            }

            commandCoder.DecodeCommand(package);

            return package;
        }
    }
}