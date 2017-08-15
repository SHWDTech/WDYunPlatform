using System.Collections.Generic;
using System.Linq;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.ProtocolCoding.Enums;
using SHWDTech.Platform.ProtocolCoding.Generics;

namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    /// <summary>
    /// 协议包
    /// </summary>
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

        public override int PackageLenth => _structureComponents.Select(p => p.Value.ComponentContent.Length).Sum() + DataComponent.ComponentContent.Length;

        /// <summary>
        /// 数据段索引
        /// </summary>
        private int _dataIndex;

        private IPackageComponent<byte[]> DataComponent { get; set; }

        public Dictionary<string, IPackageComponent<byte[]>> DataComponents { get; } = new Dictionary<string, IPackageComponent<byte[]>>();

        /// <summary>
        /// 协议包组件字典
        /// </summary>
        private readonly Dictionary<string, IPackageComponent<byte[]>> _structureComponents = new Dictionary<string, IPackageComponent<byte[]>>();

        public IPackageComponent<byte[]> this[string name]
        {
            get
            {
                if (name == "Data") return DataComponent;

                if (_structureComponents.ContainsKey(name)) return _structureComponents[name];

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

                if (!_structureComponents.ContainsKey(name))
                {
                    _structureComponents.Add(name, value);
                }
                else
                {
                    _structureComponents[name] = value;
                }
            }
        }

        public void AppendData(IPackageComponent<byte[]> component)
        {
            DataComponents.Add(component.ComponentName, component);
        }

        public override byte[] GetBytes()
        {
            var bytes = new List<byte>();

            for (var i = 0; i <= _structureComponents.Count; i++)
            {
                var componentBytes = i == _dataIndex
                    ? DataComponent.ComponentContent
                    : _structureComponents.First(obj => obj.Value.ComponentIndex == i).Value.ComponentContent;

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
            if (
                //数据段单独存放，因此_componentData的长度为协议结构长度减一
                (_structureComponents.Count + 1 != Protocol.ProtocolStructures.Count)
                || !ProtocolChecker.CheckProtocol(this)
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
