using System.Collections.Generic;
using System.Linq;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.ProtocolCoding.Coding;
using SHWDTech.Platform.ProtocolCoding.Command;
using SHWDTech.Platform.ProtocolCoding.Enums;
using SHWDTech.Platform.Utility;

namespace SHWDTech.Platform.ClssicCommandCoding
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

        public IProtocolPackage EncodeCommand(IProtocolCommand command, Dictionary<string, byte[]> paramBytes = null)
        {
            var package = new ProtocolPackage(command)
            {
                [StructureNames.CmdType] = { ComponentBytes = command.CommandTypeCode },
                [StructureNames.CmdByte] = { ComponentBytes = command.CommandCode }
            };


            foreach (var definition in command.CommandDefinitions)
            {
                package[definition.StructureName].ComponentBytes = definition.ContentBytes;
            }

            if (paramBytes != null)
            {
                foreach (var paramByte in paramBytes)
                {
                    package[paramByte.Key].ComponentBytes = paramByte.Value;
                }
            }

            var crcValue = Globals.GetUsmbcrc16(package.GetBytes(), (ushort)(package.PackageLenth - 3));
            package[StructureNames.CrcValue].ComponentBytes = Globals.Uint16ToBytes(crcValue, false);

            package.Finalization();
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
