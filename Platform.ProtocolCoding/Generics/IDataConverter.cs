namespace SHWDTech.Platform.ProtocolCoding.Generics
{
    /// <summary>
    /// 数据转换器接口
    /// </summary>
    public interface IDataConverter<T>
    {
        /// <summary>
        /// 协议数据解码
        /// </summary>
        /// <param name="packageComponent"></param>
        /// <returns></returns>
        object DecodeComponentData(IPackageComponent<T> packageComponent);

        /// <summary>
        /// 协议数据编码
        /// </summary>
        /// <param name="packageComponent"></param>
        /// <param name="componentData"></param>
        /// <returns></returns>
        byte[] EncodeComponentData(IPackageComponent<T> packageComponent, object componentData);
    }
}
