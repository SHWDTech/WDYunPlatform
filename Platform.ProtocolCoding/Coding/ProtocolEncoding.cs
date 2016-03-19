using System.Collections.Generic;
using System.Linq;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding.Command;
using SHWDTech.Platform.Utility;

namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    /// <summary>
    /// 协议编解码器
    /// </summary>
    public static class ProtocolEncoding
    {
        /// <summary>
        /// 协议编码
        /// </summary>
        /// <param name="command">指定的指令</param>
        /// <returns>协议字节流</returns>
        public static byte[] EncodeProtocol( ProtocolCommand command)
        {
            var package = UnityFactory.Resolve<ICommandCoding>().EncodeCommand(command);

            var byteList = new List<byte>();
            var structures = command.Protocol.ProtocolStructures;

            for (var i = 0; i < structures.Count; i++)
            {
                var structure = structures.First(struc => struc.ComponentIndex == i);
                byteList.AddRange(package[structure.ComponentName].ComponentData);
            }

            return byteList.ToArray();
        }

        /// <summary>
        /// 协议解码
        /// </summary>
        /// <param name="protocolBytes">字节流</param>
        /// <param name="matchedProtocol">对应的协议</param>
        /// <returns>协议解析结果</returns>
        public static ProtocolPackage DecodeProtocol(byte[] protocolBytes, Protocol matchedProtocol)
        {
            var result = new ProtocolPackage();

            var structures = matchedProtocol.ProtocolStructures.ToList();

            var currentIndex = 0;

            foreach (var structure in structures)
            {
                var component = new PackageComponent
                {
                    ComponentName = structure.ComponentName,
                    DataType = structure.DataType,
                    ComponentData = protocolBytes.SubBytes(currentIndex, currentIndex + structure.ComponentDataLength)
                };

                currentIndex += structure.ComponentDataLength;

                result[structure.ComponentName] = component;
            }

            UnityFactory.Resolve<ICommandCoding>(matchedProtocol.ProtocolName).DecodeCommand(out result);

            return result;
        }
    }
}