using System;

namespace SHWDTech.Platform.Utility.ExtensionMethod
{
    public static class DateTimeExtensionMethod
    {
        public static DateTime Trim(this DateTime date, long roundTicks)
        {
            return new DateTime(date.Ticks - date.Ticks % roundTicks);
        }
    }
}
