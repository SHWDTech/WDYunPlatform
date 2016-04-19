using System;
using System.Collections.Generic;
using System.Linq;
using SHWDTech.Platform.Model.IModel;
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
        private readonly IList<IProtocolCommand> _protocolCommands = new List<IProtocolCommand>();

        public void DecodeCommand(IProtocolPackage package, IProtocol matchedProtocol)
        {
            foreach (var com in matchedProtocol.ProtocolCommands)
            {
                _protocolCommands.Add(com);
            }

            var cmdType = package[StructureNames.CmdType].ComponentBytes;

            var cmdByte = package[StructureNames.CmdByte].ComponentBytes;

            var command = GetCommand(cmdType, cmdByte);

            package.Command = command;

            var currentIndex = 0;

            var container = package[StructureNames.Data].ComponentBytes;
            foreach (var data in command.CommandDatas)
            {
                if (currentIndex + data.DataLength > container.Length)
                {
                    package.Status = PackageStatus.NoEnoughBuffer;
                    return;
                }

                var component = new PackageComponent
                {
                    ComponentName = data.DataName,
                    DataType = data.DataType,
                    ComponentBytes = container.SubBytes(currentIndex, currentIndex + data.DataLength)
                };

                currentIndex += data.DataLength;

                package[data.DataName] = component;
            }

            package.Finalization();
        }

        public IProtocolPackage EncodeCommand(IProtocolCommand command)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取协议包对应的指令
        /// </summary>
        /// <param name="cmdTypeBytes"></param>
        /// <param name="cmdBytes"></param>
        /// <returns></returns>
        private IProtocolCommand GetCommand(IEnumerable<byte> cmdTypeBytes, IEnumerable<byte> cmdBytes)
        {
            IProtocolCommand cmd = null;
            foreach (var command in _protocolCommands.Where(command => (cmdTypeBytes.SequenceEqual(command.CommandTypeCode) &&
                                                                        cmdBytes.SequenceEqual(command.CommandCode))))
            {
                cmd = command;
            }

            return cmd;
        }

        private void ProcotolCheck(IProtocolPackage package)
        {
            var calcCrc = Globals.GetUsmbcrc16(package.GetBytes(), (ushort)(package.PackageLenth - 3));

            //var protocolCrc = 
        }
    }
}
