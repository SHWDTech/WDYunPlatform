using System;
using System.Collections.Generic;
using System.Linq;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding.Enums;

namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    /// <summary>
    /// 协议包
    /// </summary>
    public class ProtocolPackage : IProtocolPackage
    {
        public bool Finalized { get; private set; }

        public int PackageLenth => _structureComponents.Sum(obj => obj.Value.ComponentBytes.Length) + DataComponent.ComponentBytes.Length;

        /// <summary>
        /// 数据段索引
        /// </summary>
        private int _dataIndex;

        public Device Device { get; set; }

        public ProtocolCommand Command { get; set; }

        public IPackageComponent DataComponent { get; private set; }

        public DateTime ReceiveDateTime { get; set; }

        public Protocol Protocol { get; set; }

        public PackageStatus Status { get; set; } = PackageStatus.UnFinalized;

        public List<string> DeliverParams => Command.CommandDeliverParams;

        /// <summary>
        /// 协议包组件字典
        /// </summary>
        private readonly Dictionary<string, IPackageComponent> _structureComponents = new Dictionary<string, IPackageComponent>();

        /// <summary>
        /// 协议数据组件字典
        /// </summary>
        private readonly Dictionary<string, IPackageComponent> _dataComponents = new Dictionary<string, IPackageComponent>(); 

        public IPackageComponent this[string name]
        {
            get
            {
                if (name == "Data") return DataComponent;

                if(_structureComponents.ContainsKey(name)) return _structureComponents[name];

                return _dataComponents.ContainsKey(name) ? _dataComponents[name] : null;
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

        public void AppendData(IPackageComponent component)
        {
            _dataComponents.Add(component.ComponentName, component);
        }

        public byte[] GetBytes()
        {
            var bytes = new List<byte>();

            for (var i = 0; i <= _structureComponents.Count; i++)
            {
                var componentBytes = i == _dataIndex
                    ? GetDataComponentBytes()
                    : _structureComponents.First(obj => obj.Value.ComponentIndex == i).Value.ComponentBytes;

                bytes.AddRange(componentBytes);
            }

            return bytes.ToArray();
        }

        /// <summary>
        /// 获取数据段字节流
        /// </summary>
        /// <returns></returns>
        private byte[] GetDataComponentBytes()
        {
            var bytes = new List<byte>();

            for (var i = 0; i < _dataComponents.Count; i++)
            {
                var dataBytes = _dataComponents.First(obj => obj.Value.ComponentIndex == i).Value.ComponentBytes;

                bytes.AddRange(dataBytes);
            }

            return bytes.ToArray();
        }

        public void Finalization()
        {
            if (
                //数据段单独存放，因此_componentData的长度为协议结构长度减一
                (_structureComponents.Count + 1 != Protocol.ProtocolStructures.Count)
                || !ProtocolChecker.CheckProtocol(this)
                || DataComponent == null
                || DataComponent.ComponentBytes.Length != Command.CommandBytesLength
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
