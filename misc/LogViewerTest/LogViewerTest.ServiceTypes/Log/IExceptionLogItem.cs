using System;

namespace LogViewerTest.Service.Log
{
    public interface IExceptionLogItem : ILogItem
    {
        Exception Exception { get; }
    }
}
