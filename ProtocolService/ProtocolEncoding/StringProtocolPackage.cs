using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHWDTech.Platform.ProtocolService.DataBase;
using SHWDTech.Platform.ProtocolService.ProtocolEncoding.Generics;

namespace SHWDTech.Platform.ProtocolService.ProtocolEncoding
{
    public sealed class StringProtocolPackage : ProtocolPackage, IProtocolPackage<string>
    {
        public StringProtocolPackage()
        {
            
        }

        public StringProtocolPackage(IProtocolCommand command)
        {
            Protocol = command.Protocol;

            Command = command;

            foreach (var structure in Protocol.ProtocolStructures)
            {
                var component = new PackageComponent<string>()
                {
                    ComponentName = structure.StructureName,
                    DataType = structure.DataType,
                    ComponentIndex = structure.StructureIndex,
                    ComponentContent = Encoding.ASCII.GetString(structure.DefaultBytes)
                };

                this[structure.StructureName] = component;
            }

            foreach (var commandData in command.CommandDatas)
            {
                var component = new PackageComponent<string>()
                {
                    ComponentName = commandData.DataName,
                    DataType = commandData.DataConvertType,
                    ComponentIndex = commandData.DataIndex
                };

                AppendData(component);
            }
        }

        /// <summary>
        /// 数据段索引
        /// </summary>
        private int _dataIndex;

        public override int PackageLenth => GetBytes().Length;

        public override int DataComponentCount => _structureComponents.Count + 1;

        public override byte[] GetBytes()
        {
            var bytes = new List<byte>();

            for (var i = 0; i <= _structureComponents.Count; i++)
            {
                var componentContent = i == _dataIndex
                    ? DataComponent.ComponentContent
                    : _structureComponents.First(obj => obj.Value.ComponentIndex == i).Value.ComponentContent;

                bytes.AddRange(Encoding.ASCII.GetBytes(componentContent));
            }

            return bytes.ToArray();
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

        public IPackageComponent<string> this[string name]
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

        public IPackageComponent<string> DataComponent { get; private set; }

        public Dictionary<string, IPackageComponent<string>> DataComponents { get; } = new Dictionary<string, IPackageComponent<string>>();

        /// <summary>
        /// 协议包组件字典
        /// </summary>
        private readonly Dictionary<string, IPackageComponent<string>> _structureComponents = new Dictionary<string, IPackageComponent<string>>();

        public void AppendData(IPackageComponent<string> component)
        {
            DataComponents.Add(component.ComponentName, component);
        }

        public string GetDataValueString(string dataValueName)
        {
            if (!DataComponents.ContainsKey(dataValueName))
            {
                return string.Empty;
            }

            return DataComponents[dataValueName].ComponentValue;
        }
    }
}
