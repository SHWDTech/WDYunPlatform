using System;
using System.Collections.Generic;
using System.Linq;
using SHWDTech.Platform.ProtocolService.DataBase;
using SHWDTech.Platform.ProtocolService.ProtocolEncoding.Generics;

namespace SHWDTech.Platform.ProtocolService.ProtocolEncoding
{
    public class BytesProtocolPackage : ProtocolPackage, IProtocolPackage<byte[]>
    {
        public BytesProtocolPackage()
        {

        }

        public BytesProtocolPackage(IProtocolCommand command)
        {
            Protocol = command.Protocol;

            Command = command;

            foreach (var structure in Protocol.ProtocolStructures)
            {
                var component = new PackageComponent<byte[]>
                {
                    ComponentName = structure.StructureName,
                    DataType = structure.DataType,
                    ComponentIndex = structure.StructureIndex,
                    ComponentContent = structure.DefaultBytes
                };

                this[structure.StructureName] = component;
            }

            foreach (var commandData in command.CommandDatas)
            {
                var component = new PackageComponent<byte[]>
                {
                    ComponentName = commandData.DataName,
                    DataType = commandData.DataConvertType,
                    ComponentIndex = commandData.DataIndex
                };

                AppendData(component);
            }
        }

        public override bool Finalized { get; protected set; }

        public override int PackageLenth => StructureComponents.Select(p => p.Value.ComponentContent.Length).Sum() + DataComponent.ComponentContent.Length;

        /// <summary>
        /// 数据段索引
        /// </summary>
        private int _dataIndex;

        public IPackageComponent<byte[]> DataComponent { get; set; }

        public Dictionary<string, IPackageComponent<byte[]>> DataComponents { get; } = new Dictionary<string, IPackageComponent<byte[]>>();

        /// <summary>
        /// 协议包组件字典
        /// </summary>
        protected readonly Dictionary<string, IPackageComponent<byte[]>> StructureComponents = new Dictionary<string, IPackageComponent<byte[]>>();

        public IPackageComponent<byte[]> this[string name]
        {
            get
            {
                if (name == "Data") return DataComponent;

                if (StructureComponents.ContainsKey(name)) return StructureComponents[name];

                return DataComponents.ContainsKey(name) ? DataComponents[name] : null;
            }
            set
            {
                if (name == "Data")
                {
                    DataComponent = value;
                    _dataIndex = value.ComponentIndex;
                    return;
                }

                if (!StructureComponents.ContainsKey(name))
                {
                    StructureComponents.Add(name, value);
                }
                else
                {
                    StructureComponents[name] = value;
                }
            }
        }

        public void AppendData(IPackageComponent<byte[]> component)
        {
            DataComponents.Add(component.ComponentName, component);
        }

        public override void SetupProtocolData()
        {
            if (ClientSource == null) return;
            ProtocolData = new ProtocolData
            {
                Business = ClientSource.BusinessName,
                DeviceNodeId = ClientSource.ClientNodeId,
                ProtocolContent = GetBytes(),
                ProtocolId = Guid.Parse(Protocol.GetIdString()),
                PackageDateTime = DateTime.Now,
            };
            ProtocolData.Length = ProtocolData.ProtocolContent.Length;
            ProtocolData.ProtocolString = ProtocolData.ProtocolContent.ToHexString();
        }

        public string GetDataValueString(string dataValueName)
        {
            throw new NotImplementedException();
        }

        public virtual byte[] GetCrcBytes()
        {
            var allBytes =  GetBytes();
            var finalBytes = new byte[allBytes.Length - 3];
            Array.Copy(allBytes, 0, finalBytes, 0, finalBytes.Length);
            return finalBytes;
        }

        public override byte[] GetBytes()
        {
            var bytes = new List<byte>();

            for (var i = 0; i <= StructureComponents.Count; i++)
            {
                var componentBytes = i == _dataIndex
                    ? DataComponent.ComponentContent
                    : StructureComponents.First(obj => obj.Value.ComponentIndex == i).Value.ComponentContent;

                bytes.AddRange(componentBytes);
            }

            return bytes.ToArray();
        }

        /// <summary>
        /// 合并数据段字节流
        /// </summary>
        /// <returns></returns>
        public void CombineDataComponentBytes()
        {
            var bytes = new List<byte>();

            for (var i = 0; i < DataComponents.Count; i++)
            {
                var dataBytes = DataComponents.First(obj => obj.Value.ComponentIndex == i).Value.ComponentContent;

                bytes.AddRange(dataBytes);
            }

            DataComponent.ComponentContent = bytes.ToArray();
        }

        public override void Finalization()
        {
            if (!ProtocolChecker.CheckProtocol(this))
            {
                Status = PackageStatus.ValidationFailed;
                return;
            }
            if (
                //数据段单独存放，因此_componentData的长度为协议结构长度减一
                (StructureComponents.Count + 1 != Protocol.ProtocolStructures.Count)
                || DataComponent == null
                || (Command.DataOrderType == DataOrderType.Order && DataComponent.ComponentContent.Length != Command.ReceiveBytesLength)
            )
            {
                Status = PackageStatus.InvalidPackage;
                return;
            }

            Status = PackageStatus.Finalized;

            Finalized = true;
        }
    }
}
