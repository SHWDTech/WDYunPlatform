using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHWDTech.Platform.Common.Model;

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
