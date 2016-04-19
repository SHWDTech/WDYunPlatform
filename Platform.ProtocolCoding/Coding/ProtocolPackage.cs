using System;
using System.Collections.Generic;
using System.Linq;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.ProtocolCoding.Enums;

namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    /// <summary>
    /// 协议包
    /// </summary>
    public class ProtocolPackage : IProtocolPackage
    {
        public bool Finalized { get; private set; }

        public int PackageLenth => _componentData.Sum(obj => obj.Value.ComponentBytes.Length);

        /// <summary>
        /// 数据段索引
        /// </summary>
        private int _dataIndex;

        public IDevice Device { get; set; }

        public IProtocolCommand Command { get; set; }

        public IPackageComponent DataComponent { get; private set; }

        public DateTime ReceiveDateTime { get; set; }

        public IProtocol Protocol { get; set; }

        public PackageStatus Status { get; set; } = PackageStatus.UnFinalized;

        public string DeliverParams { get; set; }

        /// <summary>
        /// 协议包组件字典
        /// </summary>
        private readonly Dictionary<string, IPackageComponent> _componentData = new Dictionary<string, IPackageComponent>();

        public IPackageComponent this[string name]
        {
            get
            {
                if (name == "Data") return DataComponent;

                return !_componentData.ContainsKey(name) ? null : _componentData[name];
            }
            set
            {
                if (name == "Data")
                {
                    DataComponent = value;
                    _dataIndex = value.ComponentIndex;
                    return;
                }

                if (!_componentData.ContainsKey(name))
                {
                    _componentData.Add(name, value);
                }
                else
                {
                    _componentData[name] = value;
                }
            }
        }

        public byte[] GetBytes()
        {
            var bytes = new List<byte>();

            for (var i = 0; i <= _componentData.Count; i++)
            {
                var component = i == _dataIndex 
                    ? DataComponent 
                    : _componentData.First(obj => obj.Value.ComponentIndex == i).Value;

                bytes.AddRange(component.ComponentBytes);
            }

            return bytes.ToArray();
        }

        public void Finalization()
        {
            //数据段单独存放，因此_componentData的长度为协议结构长度减一
            if (_componentData.Count + 1 != Protocol.ProtocolStructures.Count) return;

            if (DataComponent == null || DataComponent.ComponentBytes.Length != Command.CommandBytesLength) return;

            Status = PackageStatus.Finalized;
            Finalized = true;
        }
    }
}
