using System.Text;

namespace SHWDTech.Platform.ProtocolService
{
    public static class ProtocolExtensions
    {
        public static string ToHexString(this byte[] data)
        {
            var sb = new StringBuilder(data.Length * 3);
            foreach (var b in data)
            {
                var Char = $"0x{b:X2} ";
                sb.Append(Char);
            }
            return sb.ToString();
        }
    }
}
