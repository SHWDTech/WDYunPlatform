using System;

namespace MvcWebComponents
{
    public class ExecuteResult
    {
        public Exception Exception { get; private set; }

        public ExecuteResult(Exception exception)
        {
            Exception = exception;
        }
    }
}
