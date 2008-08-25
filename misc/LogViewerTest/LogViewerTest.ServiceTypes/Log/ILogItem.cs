using System;

namespace LogViewerTest.Service.Log
{
    public interface ILogItem : ILogId
    {
        Guid Category { get; }
        Guid Event { get; }
        Guid Severity { get; }
        Guid Incident { get; }
        string Title { get; }
        string Message { get; }        
    }
}
