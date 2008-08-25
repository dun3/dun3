using System;
using System.Collections.Generic;

namespace LogViewerTest.Service.Log
{
    public interface ILogService
    {
        ILogId FindLastLogId();
        ILogId FindLastLogIdByIncidentId(Guid incidentId);
        bool IsLastLogId(ILogId id);
        bool IsLastLogIdByIncidentId(Guid incidentId, ILogId id);
        IList<ILogItem> FindLogItemzByIncidentId(Guid incidentId); 
        IList<ISummaryLogItem> FindSummaryLogItemzByIncidentId(Guid incidentId);
        ILogItem FindLogItem(ILogId id);
        ILogItem CreateLogItem(ILogItem logItem);
        IExceptionLogItem CreateExceptionLogItem(IExceptionLogItem exceptionLogItem);        
    }
}
