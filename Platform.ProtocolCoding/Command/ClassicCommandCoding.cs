using System;
using System.Collections.Generic;
using System.Linq;
using SHWD.Platform.Repository;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding.Coding;
using SHWDTech.Platform.ProtocolCoding.Enums;
using SHWDTech.Platform.Utility;

namespace SHWDTech.Platform.ProtocolCoding.Command
{
    /// <summary>
    /// 经典协议解析模块
    /// </summary>
    public class ClassicCommandCoding : ICommandCoding
    {
        /// <summary>
        /// 协议包含的指令
        /// </summary>
        private readonly IList<ProtocolCommand> _protocolCommands = new List<ProtocolCommand>();

        public void DecodeCommand(ProtocolPackage package, Protocol matchedProtocol)
        {
            foreach (var com in matchedProtocol.ProtocolCommands)
            {
                _protocolCommands.Add(com);
            }

            var cmdType = package[StructureNames.CmdType].ComponentData;

            var cmdByte = package[StructureNames.CmdByte].ComponentData;

            var command = GetCommand(cmdType, cmdByte);

            var currentIndex = 0;

            var container = package[StructureNames.Data].ComponentData;
            foreach (var data in command.CommandDatas)
            {
                if (currentIndex + data.DataLength > container.Length) return;

                var component = new PackageComponent
                {
                    ComponentName = data.DataName,
                    DataType = data.DataType,
                    ComponentData = container.SubBytes(currentIndex, currentIndex + data.DataLength)
                };

                currentIndex += data.DataLength;

                package[data.DataName] = component;
            }

            if (command.CommandCategory != CommandCategory.Authentication) return;
            var nodeId = DataConvert.DecodeComponentData(package[StructureNames.NodeId]).ToString();
            var device = DbRepository.Repo<DeviceRepository>().GetDeviceByNodeId(nodeId).FirstOrDefault();
            if (device != null) package.Device = device;

            package.Finalization();
        }

        public ProtocolPackage EncodeCommand(ProtocolCommand command)
        {
            throw new NotImplementedException();
        }

        public ProtocolPackage DecodeAuthentication(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取协议包对应的指令
        /// </summary>
        /// <param name="cmdTypeBytes"></param>
        /// <param name="cmdBytes"></param>
        /// <returns></returns>
        private ProtocolCommand GetCommand(IEnumerable<byte> cmdTypeBytes, IEnumerable<byte> cmdBytes)
        {
            ProtocolCommand cmd = null;
            foreach (var command in _protocolCommands.Where(command => (cmdTypeBytes.SequenceEqual(command.CommandTypeCode) &&
                                                                        cmdBytes.SequenceEqual(command.CommandCode))))
            {
                cmd = command;
            }

            return cmd;
        }
    }
}
