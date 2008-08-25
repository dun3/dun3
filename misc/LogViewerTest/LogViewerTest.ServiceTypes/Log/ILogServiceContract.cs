using System;
using System.Collections.Generic;
using System.ServiceModel;
using LogViewerTest.Service.Log.DTO;

namespace LogViewerTest.Service.Log
{
    [ServiceContract]
    public interface ILogServiceContract
    {
        [OperationContract]
        LogIdDTO FindLastLogId();

        [OperationContract]
        LogIdDTO FindLastLogIdByIncidentId(Guid incidentId);

        [OperationContract]
        bool IsLastLogId(LogIdDTO id);

        [OperationContract]
        bool IsLastLogIdByIncidentId(Guid incidentId, LogIdDTO id);

        [OperationContract]
        List<SummaryLogItemDTO> FindSummaryLogItemzByIncidentId(Guid incidentId);

        [OperationContract]
        LogItemDTO FindLogItem(LogIdDTO id);

        [OperationContract]
        LogItemDTO CreateLogItem(LogItemDTO logItem);

        [OperationContract]
        ExceptionLogItemDTO CreateExceptionLogItem(ExceptionLogItemDTO exceptionLogItem);
    }
}
