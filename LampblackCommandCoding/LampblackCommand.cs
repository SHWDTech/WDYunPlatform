using System.Collections.Generic;
using System.Linq;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.ProtocolCoding.Coding;
using SHWDTech.Platform.ProtocolCoding.Command;
using SHWDTech.Platform.ProtocolCoding.Enums;
using SHWDTech.Platform.Utility;

namespace SHWDTech.Platform.LampblackCommandCoding
{
    public class LampblackCommand : ICommandCoding
    {
        public void DecodeCommand(IProtocolPackage package)
        {
            var container = package[StructureNames.Data].ComponentBytes;

            switch (package.Command.DataOrderType)
            {
                case DataOrderType.Order:
                    DecodeOrderedData(package, container);
                    break;
                case DataOrderType.Random:
                    DecodeRandomData(package, container);
                    break;
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
            package[StructureNames.CRCValue].ComponentBytes = Globals.Uint16ToBytes(crcValue, false);

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

        /// <summary>
        /// 解码固定顺序数据段
        /// </summary>
        /// <param name="package"></param>
        /// <param name="container"></param>
        private void DecodeOrderedData(IProtocolPackage package, byte[] container)
        {
            var currentIndex = 0;

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
                    DataType = data.DataConvertType,
                    ComponentIndex = data.DataIndex,
                    ComponentBytes = container.SubBytes(currentIndex, currentIndex + data.DataLength)
                };

                currentIndex += data.DataLength;

                package.AppendData(component);
            }
        }

        /// <summary>
        /// 解码自由组合数据段
        /// </summary>
        /// <param name="package"></param>
        /// <param name="container"></param>
        private void DecodeRandomData(IProtocolPackage package, byte[] container)
        {
            var currentIndex = 0;

            while (currentIndex < container.Length)
            {
                var dataFlag = container[currentIndex + 1] & 0x7F; //0x7F = 0111 1111

                var data = package.Command.CommandDatas.First(obj => obj.DataFlag == dataFlag);

                if (currentIndex + data.DataLength > container.Length)
                {
                    package.Status = PackageStatus.NoEnoughBuffer;
                    return;
                }

                var component = new PackageComponent
                {
                    ComponentName = data.DataName,
                    DataType = data.DataConvertType,
                    ComponentIndex = data.DataIndex,
                    ComponentBytes = container.SubBytes(currentIndex, currentIndex + data.DataLength + 2)
                };

                currentIndex += data.DataLength + 2;

                package.AppendData(component);
            }
        }
    }
}
