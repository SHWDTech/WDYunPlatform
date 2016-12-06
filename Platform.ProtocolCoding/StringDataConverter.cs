using SHWDTech.Platform.ProtocolCoding.Generics;

namespace SHWDTech.Platform.ProtocolCoding
{
    public class StringDataConverter : DataConverter<string>
    {
        public override object DecodeComponentData(IPackageComponent<string> packageComponent)
        {
            return packageComponent.ComponentContent;
        }

        public override byte[] EncodeComponentData(IPackageComponent<string> packageComponent, object componentData)
        {
            var convertMethod = Converter.GetMethod($"{packageComponent.DataType}Encode");

            return (byte[])convertMethod.Invoke(convertMethod, new[] { componentData });
        }

        /// <summary>
        /// 解码NodeId
        /// </summary>
        /// <param name="nodeIdString"></param>
        /// <returns></returns>
        public static string NodeIdDecode(string nodeIdString)
            => nodeIdString.Substring(10, 4);

    }
}
