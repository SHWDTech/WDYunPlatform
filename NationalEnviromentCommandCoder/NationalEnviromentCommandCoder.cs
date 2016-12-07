using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding;
using SHWDTech.Platform.ProtocolCoding.Coding;
using SHWDTech.Platform.ProtocolCoding.Enums;
using SHWDTech.Platform.ProtocolCoding.Generics;

namespace SHWDTech.Platform.NationalEnviromentCommandCoder
{
    public class NationalEnviromentCommandCoder : ICommandCoder<string>
    {
        private readonly StringPackageDeliver _deliver = new StringPackageDeliver();

        public IDataConverter<string> DataConverter { get; set; } = new StringDataConverter();

        public void DecodeCommand(IProtocolPackage<string> package)
        {
            var container = package[StructureNames.Data].ComponentContent;

            container = container.Replace("CP=&&", string.Empty).Replace("&&", string.Empty);

            var dataGroups = container.Split(';');

            var commandDataDicts = (from dataGroup in dataGroups
                                where dataGroup.Contains(",")
                                from data in dataGroup.Split(',')
                                select data.Split('='))
                                .ToDictionary(dataKeyValuePair => dataKeyValuePair[0], dataKeyValuePair => dataKeyValuePair[1]);

            foreach (var commandDataDic in commandDataDicts)
            {
                var commandData = package.Command.CommandDatas.FirstOrDefault(obj => obj.DataName == commandDataDic.Key);
                if (commandData == null) continue;
                var component = new PackageComponent<string>
                {
                    ComponentName = commandDataDic.Key,
                    DataType = commandData.DataConvertType,
                    ComponentIndex = commandData.DataIndex,
                    ComponentContent = commandDataDic.Value
                };

                package.AppendData(component);
            }

            package.DeviceNodeId = (string)DataConverter.DecodeComponentData(package[StructureNames.NodeId]);

            package.Finalization();
        }

        public void DoDelive(IProtocolPackage package, IPackageSource source)
        {
            var bytesPackage = (IProtocolPackage<string>)package;
            if (bytesPackage == null) return;
            _deliver.Delive(bytesPackage, source);
        }

        public void DetectCommand(IProtocolPackage<string> package, IProtocol matchedProtocol)
        {
            foreach (var command in matchedProtocol.ProtocolCommands.Where(command =>
            (package[StructureNames.CmdType].ComponentValue == Encoding.ASCII.GetString(command.CommandTypeCode))
            && (package[StructureNames.CmdByte].ComponentValue == Encoding.ASCII.GetString(command.CommandCode))))
            {
                package.Command = command;
            }
        }

        public IProtocolPackage EncodeCommand(IProtocolCommand command, Dictionary<string, byte[]> paramBytes = null)
        {
            throw new NotImplementedException();
        }

        public IProtocolPackage DecodeProtocol(byte[] bufferBytes, Protocol matchedProtocol)
        {
            var protocolString = Encoding.ASCII.GetString(bufferBytes);

            var package = new StringProtocolPackage() { Protocol = matchedProtocol, ReceiveDateTime = DateTime.Now };

            var structures = matchedProtocol.ProtocolStructures.ToList();

            var currentIndex = 0;

            var knownStructureLength = structures.Sum(protocolStructure => protocolStructure.StructureDataLength);

            for (var i = 0; i < structures.Count; i++)
            {
                var structure = structures.First(obj => obj.StructureIndex == i);

                var componentDataLength = structure.StructureName == StructureNames.Data && structure.StructureDataLength == 0
                    ? int.Parse(package["ContentLength"].ComponentContent) - knownStructureLength + 12
                    : structure.StructureDataLength;

                if (currentIndex + componentDataLength > protocolString.Length)
                {
                    package.Status = PackageStatus.NoEnoughBuffer;
                    return package;
                }

                if (structure.StructureName == StructureNames.Data)
                {
                    DetectCommand(package, matchedProtocol);
                    if (package.Command == null)
                    {
                        package.Status = PackageStatus.InvalidHead;
                        return package;
                    }
                    componentDataLength = package.Command.ReceiveBytesLength == 0 ? componentDataLength : package.Command.ReceiveBytesLength;
                }

                var component = new PackageComponent<string>
                {
                    ComponentName = structure.StructureName,
                    DataType = structure.DataType,
                    ComponentIndex = structure.StructureIndex,
                    ComponentContent = protocolString.Substring(currentIndex, componentDataLength)
                };

                component.ComponentValue = ParseStructureValue(component.ComponentContent);

                currentIndex += componentDataLength;

                package[structure.StructureName] = component;
            }

            DecodeCommand(package);

            return package;
        }

        private static string ParseStructureValue(string structureString)
        {
            if (structureString.Contains("CP"))
            {
                return structureString;
            }

            if (structureString.Contains(";"))
            {
                structureString = structureString.Replace(";", string.Empty);
            }

            if (structureString.Contains("="))
            {
                structureString = structureString.Split('=')[1];
            }

            return structureString;
        }
    }
}
