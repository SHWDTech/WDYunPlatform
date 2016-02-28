using System;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    public interface IMonitorData
    {
        long MonitorDataId { get; set; }

        Protocol Protocol { get; set; }

        SysDomain MonitorDataDomain { get; set; }

        Device Device { get; set; }

        int MonitorDataType { get; set; }

        double MonitorDataValue { get; set; }

        DateTime UpdateTime { get; set; }

        bool DataIsValid { get; set; }
    }
}
