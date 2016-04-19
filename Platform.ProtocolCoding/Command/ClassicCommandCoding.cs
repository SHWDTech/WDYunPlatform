using System;
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
        public void DecodeCommand(IProtocolPackage package)
        {
            var currentIndex = 0;

            var container = package[StructureNames.Data].ComponentBytes;

            foreach (var data in package.Command.CommandDatas)
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

        public void DetectCommand(IProtocolPackage package, IProtocol matchedProtocol)
        {
            foreach (var command in matchedProtocol.ProtocolCommands.Where(command =>
            (package[StructureNames.CmdType].ComponentBytes.SequenceEqual(command.CommandTypeCode))
            && (package[StructureNames.CmdByte].ComponentBytes.SequenceEqual(command.CommandCode))))
            {
                package.Command = command;
            }
        }
    }
}
