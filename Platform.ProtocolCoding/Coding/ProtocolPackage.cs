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
        public ProtocolPackage()
        {
            _componentData = new Dictionary<string, IPackageComponent>();
        }

        public bool Finalized { get; private set; }

        public int PackageLenth => _componentData.Sum(obj => obj.Value.ComponentData.Length);

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
        private readonly Dictionary<string, IPackageComponent> _componentData;

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

            for (int i = 0; i < _componentData.Count; i++)
            {
                var comp = _componentData.First(obj => obj.Value.ComponentIndex == i).Value;
                //if(comp.ComponentIndex)
            }

            return null;
        }

        public void Finalization()
        {
            Finalized = true;
        }
    }
}
