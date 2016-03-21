using System;
using System.Collections.Generic;
using System.Linq;

namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    /// <summary>
    /// 协议包
    /// </summary>
    public class ProtocolPackage : IProtocolPackage
    {
        public ProtocolPackage()
        {
            _componentData = new Dictionary<string, PackageComponent>();
        }

        public bool Finalized { get; private set; }

        public int PackageLenth { get; private set; } = -1;

        public Guid DeviceGuid { get; set; }

        public PackageComponent DataComponent { get; private set; }

        public DateTime ReceiveDateTime { get; set; }

        /// <summary>
        /// 协议包组件字典
        /// </summary>
        private readonly Dictionary<string, PackageComponent> _componentData;

        public PackageComponent this[string name]
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

        public void Finalization()
        {
            PackageLenth = _componentData.Sum(obj => obj.Value.ComponentData.Length);
            Finalized = true;
        }
    }
}
