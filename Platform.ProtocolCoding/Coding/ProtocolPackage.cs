using System;
using System.Collections.Generic;

namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    /// <summary>
    /// 协议包
    /// </summary>
    public class ProtocolPackage : IProtocolPackage
    {
        public ProtocolPackage()
        {
            _componentData = new Dictionary<string, Component>();
        }

        public Guid DeviceGuid { get; set; }

        public DateTime ReceiveDateTime { get; set; }

        /// <summary>
        /// 协议包组件字典
        /// </summary>
        private readonly Dictionary<string, Component> _componentData;

        public Component this[string name]
        {
            get
            {
                return !_componentData.ContainsKey(name) ? null : _componentData[name];
            }
            set
            {
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
    }
}
