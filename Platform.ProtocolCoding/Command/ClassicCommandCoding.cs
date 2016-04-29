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

            for (var i = 0; i < package.Command.CommandDatas.Count; i++)
            {
                var data = package.Command.CommandDatas.First(obj => obj.DataIndex == i);

                if (currentIndex + data.DataLength > container.Length)
                {
                    package.Status = PackageStatus.NoEnoughBuffer;
                    return;
                }

                var component = new PackageComponent
                {
                    ComponentName = data.DataName,
                    DataType = data.DataType,
                    ComponentIndex = data.DataIndex,
                    ComponentBytes = container.SubBytes(currentIndex, currentIndex + data.DataLength)
                };

                currentIndex += data.DataLength;

                package.AppendData(component);
            }

            package.Finalization();
        }

        public IProtocolPackage EncodeCommand(IProtocolCommand command)
        {
            var package = new ProtocolPackage(command);

            foreach (var definition in command.CommandDefinitions)
            {
                package[definition.StructureName].ComponentBytes = definition.ContentBytes;
            }

            return package;
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
