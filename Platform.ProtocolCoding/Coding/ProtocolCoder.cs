using System.Collections.Generic;
using System.Linq;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.Utility;

namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    /// <summary>
    /// 协议编解码器
    /// </summary>
    public class ProtocolCoder
    {
        /// <summary>
        /// 协议编码
        /// </summary>
        /// <param name="package">协议结果</param>
        /// <param name="assignedProtocol">指定的协议</param>
        /// <returns>协议字节流</returns>
        public byte[] EncodeProtocol(ProtocolPackage package, Protocol assignedProtocol)
        {
            var byteList = new List<byte>();
            for (var i = 0; i < assignedProtocol.ProtocolStructures.Count; i++)
            {
                var structure = assignedProtocol.ProtocolStructures.First(struc => struc.ComponentIndex == i);
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
        public ProtocolPackage DecodeProtocol(byte[] protocolBytes, Protocol matchedProtocol)
        {
            var result = new ProtocolPackage();

            var structures = matchedProtocol.ProtocolStructures.ToList();

            var currentIndex = 0;

            foreach (var structure in structures)
            {
                var component = new Component
                {
                    ComponentName = structure.ComponentName,
                    DataType = structure.DataType,
                    ComponentData = protocolBytes.SubBytes(currentIndex, currentIndex + structure.ComponentDataLength)
                };

                currentIndex += structure.ComponentDataLength;

                result[structure.ComponentName] = component;
            }

            return result;
        }
    }
}