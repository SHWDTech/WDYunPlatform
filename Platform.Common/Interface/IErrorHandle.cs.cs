using SHWDTech.Platform.Common.Model;
using System;
using System.Collections.Generic;

namespace SHWDTech.Platform.Common.Interface
{
    public interface IErrorHandle
    {
        string GetMessage(Exception ex, bool writeLog, bool includeDebugMessage);

        string GetErrorJson(Exception ex);

        object GetExceptionObject(Exception ex);

        IEnumerable<Exception> GetExceptionList(Exception ex);

        WdtMessage GetWdtMessage(Exception ex);
    }
}