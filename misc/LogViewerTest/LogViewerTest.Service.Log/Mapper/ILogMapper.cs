using System;
using System.Collections.Generic;

namespace LogViewerTest.Service.Log.Mapper
{
    internal interface ILogMapper
    {
        long FindLastLogId();
        long FindLastLogIdByIncidentId(Guid incidentId);
        bool IsLastLogId(long id);
        bool IsLastLogIdByIncidentId(Guid incidentId, long id);
        List<ILogItem> FindLogItemzByIncidentId(Guid incidentId);
        ILogItem FindLogItem(long id);
        LogItem CreateLogItem(Guid categoryGuid, Guid eventGuid, Guid severityGuid, Guid incident, string title, string message);
        ExceptionLogItem CreateExceptionLogItem(Guid categoryGuid, Guid eventGuid, Guid severityGuid, Guid incident, Exception exception);
    }
}
