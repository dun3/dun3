using System;

namespace LogViewerTest.Service.Log
{
    public interface ILogId
    {
        long Id { get; } // Using long as Id, because LogItems happen in a natural chronological  order
    }
}
