using System;
using System.Collections.Generic;
using System.Linq;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding;
using SHWDTech.Platform.ProtocolCoding.Coding;
using SHWDTech.Platform.ProtocolCoding.Enums;
using SHWDTech.Platform.ProtocolCoding.Generics;
using SHWDTech.Platform.Utility;

namespace SHWDTech.Platform.LampblackCommandCoding
{
    public class LampblackCommand : ICommandCoder<byte[]>
    {
        private readonly BytesPackageDeliver _deliver = new BytesPackageDeliver();

        public IDataConverter<byte[]> DataConverter { get; set; } = new BytesDataConverter();

        public IProtocolPackage DecodeProtocol(byte[] bufferBytes, Protocol matchedProtocol)
        {
            var package = new BytesProtocolPackage() { Protocol = matchedProtocol, ReceiveDateTime = DateTime.Now };

            var structures = matchedProtocol.ProtocolStructures.ToList();

            var currentIndex = 0;

            for (var i = 0; i < structures.Count; i++)
            {
                var structure = structures.First(obj => obj.StructureIndex == i);

                //协议中，数据段如果是自由组织的形式，那么数据库中设置数据段长度为零。解码时，按照协议中的DataLength段的值解码数据段。
                var componentDataLength = structure.StructureName == StructureNames.Data && structure.StructureDataLength == 0
                    ? Globals.BytesToInt16(package["DataLength"].ComponentContent, 0, true)
                    : structure.StructureDataLength;

                if (currentIndex + componentDataLength > bufferBytes.Length)
                {
                    package.Status = PackageStatus.NoEnoughBuffer;
                    return package;
                }

                if (structure.StructureName == StructureNames.Data)
                {
                    DetectCommand(package, matchedProtocol);
                    componentDataLength = package.Command.ReceiveBytesLength == 0 ? componentDataLength : package.Command.ReceiveBytesLength;
                }

                var component = new PackageComponent<byte[]>
                {
                    ComponentName = structure.StructureName,
                    DataType = structure.DataType,
                    ComponentIndex = structure.StructureIndex,
                    ComponentContent = bufferBytes.SubBytes(currentIndex, currentIndex + componentDataLength)
                };

                currentIndex += componentDataLength;

                package[structure.StructureName] = component;
            }

            DecodeCommand(package);

            return package;
        }

        public void DecodeCommand(IProtocolPackage<byte[]> package)
        {
            var container = package[StructureNames.Data].ComponentContent;

            switch (package.Command.DataOrderType)
            {
                case DataOrderType.Order:
                    DecodeOrderedData(package, container);
                    break;
                case DataOrderType.Random:
                    DecodeRandomData(package, container);
                    break;
            }

            package.DeviceNodeId = (string)DataConverter.DecodeComponentData(package["NodeId"]);
            package.Finalization();
        }

        public IProtocolPackage EncodeCommand(IProtocolCommand command, Dictionary<string, byte[]> paramBytes = null)
        {
            var package = new BytesProtocolPackage(command)
            {
                [StructureNames.CmdType] = { ComponentContent = command.CommandTypeCode },
                [StructureNames.CmdByte] = { ComponentContent = command.CommandCode }
            };


            foreach (var definition in command.CommandDefinitions)
            {
                package[definition.StructureName].ComponentContent = definition.ContentBytes;
            }

            if (paramBytes != null)
            {
                foreach (var paramByte in paramBytes)
                {
                    package[paramByte.Key].ComponentContent = paramByte.Value;
                }
            }

            var crcValue = Globals.GetUsmbcrc16(package.GetBytes(), (ushort)(package.PackageLenth - 3));
            package[StructureNames.CRCValue].ComponentContent = Globals.Uint16ToBytes(crcValue, false);

            package.Finalization();
            return package;
        }

        public void DetectCommand(IProtocolPackage<byte[]> package, IProtocol matchedProtocol)
        {
            foreach (var command in matchedProtocol.ProtocolCommands.Where(command =>
            (package[StructureNames.CmdType].ComponentContent.SequenceEqual(command.CommandTypeCode))
            && (package[StructureNames.CmdByte].ComponentContent.SequenceEqual(command.CommandCode))))
            {
                package.Command = command;
            }
        }

        /// <summary>
        /// 解码固定顺序数据段
        /// </summary>
        /// <param name="package"></param>
        /// <param name="container"></param>
        private void DecodeOrderedData(IProtocolPackage<byte[]> package, byte[] container)
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

                var component = new PackageComponent<byte[]>
                {
                    ComponentName = data.DataName,
                    DataType = data.DataConvertType,
                    ComponentIndex = data.DataIndex,
                    ComponentContent = container.SubBytes(currentIndex, currentIndex + data.DataLength)
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
        private void DecodeRandomData(IProtocolPackage<byte[]> package, byte[] container)
        {
            var currentIndex = 0;

            while (currentIndex < container.Length)
            {
                var dataIndex = currentIndex + 2;

                var flagIndex = currentIndex + 1;

                var dataFlag = container[flagIndex] & 0x7F; //0x7F = 0111 1111

                var data = package.Command.CommandDatas.First(obj => obj.DataFlag == dataFlag);

                if (currentIndex + data.DataLength > container.Length)
                {
                    package.Status = PackageStatus.NoEnoughBuffer;
                    return;
                }

                var channel = container[currentIndex];

                var component = new PackageComponent<byte[]>
                {
                    ComponentName = $"{data.DataName}-{channel}",
                    ComponentChannel = channel,
                    CommandData = data,
                    DataValueType = data.DataValueType,
                    DataType = data.DataConvertType,
                    ComponentIndex = data.DataIndex,
                    ValidFlag = container[flagIndex],
                    ComponentContent = container.SubBytes(dataIndex, dataIndex + data.DataLength)
                };

                currentIndex = data.DataLength + dataIndex;

                package.AppendData(component);
            }
        }

        public void DoDelive(IProtocolPackage package, IPackageSource source)
        {
            var bytesPackage = (IProtocolPackage<byte[]>)package;
            if (bytesPackage == null) return;
            _deliver.Delive(bytesPackage, source);
        }
    }
}
