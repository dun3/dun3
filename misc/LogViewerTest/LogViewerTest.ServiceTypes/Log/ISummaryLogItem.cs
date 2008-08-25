using System;

namespace LogViewerTest.Service.Log
{
    public interface ISummaryLogItem : ILogId
    {
        Guid Incident { get; }
        string Summary { get; }
    }
}
