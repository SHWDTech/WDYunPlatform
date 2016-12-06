using System;

namespace SHWDTech.Platform.ProtocolCoding.Generics
{
    public class DataConverter<T> : IDataConverter<T>
    {
        /// <summary>
        /// 转换器类型
        /// </summary>
        internal Type Converter => _converter ?? (_converter = GetType());

        private Type _converter;

        public virtual object DecodeComponentData(IPackageComponent<T> packageComponent)
        {
            var convertMethod = _converter.GetMethod($"{packageComponent.DataType}Decode");

            return convertMethod.Invoke(convertMethod, new object[] { packageComponent });
        }

        public virtual byte[] EncodeComponentData(IPackageComponent<T> packageComponent, object componentData)
        {
            var convertMethod = _converter.GetMethod($"{packageComponent.DataType}Encode");

            return (byte[])convertMethod.Invoke(convertMethod, new[] { componentData });
        }
    }
}
