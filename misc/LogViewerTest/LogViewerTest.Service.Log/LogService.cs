using System;
using System.Linq;
using System.Collections.Generic;
using LogViewerTest.Service.Log.DTO;
using LogViewerTest.Framework.DTO;
using LogViewerTest.Service.Log.Mapper;

namespace LogViewerTest.Service.Log
{
    internal class LogService : ILogService, ILogServiceContract
    {
        private ILogMapper m_mapper = new FakeLogMapper();
        public void SetMapper(ILogMapper mapper)
        {
            m_mapper = mapper;
        }

        #region ILogService Members

        public ILogId FindLastLogId()
        {
            LogItem id = new LogItem();
            id.Id = m_mapper.FindLastLogId();
            return id;
        }

        public ILogId FindLastLogIdByIncidentId(Guid incidentId)
        {
            LogItem id = new LogItem();
            id.Id = m_mapper.FindLastLogIdByIncidentId(incidentId);
            return id;
        }

        public bool IsLastLogId(ILogId id)
        {
            return m_mapper.IsLastLogId(id.Id);
        }

        public bool IsLastLogIdByIncidentId(Guid incidentId, ILogId id)
        {
            return m_mapper.IsLastLogIdByIncidentId(incidentId, id.Id);
        }

        public IList<ILogItem> FindLogItemzByIncidentId(Guid incidentId)
        {
            return m_mapper.FindLogItemzByIncidentId(incidentId);
        }

        public IList<ISummaryLogItem> FindSummaryLogItemzByIncidentId(Guid incidentId)
        {
            return m_mapper.FindLogItemzByIncidentId(incidentId).Cast<ISummaryLogItem>().ToList();
        }

        public ILogItem FindLogItem(ILogId id)
        {
            return m_mapper.FindLogItem(id.Id);
        }

        public ILogItem CreateLogItem(ILogItem logItem)
        {
            return m_mapper.CreateLogItem(logItem.Category, logItem.Event, logItem.Severity, logItem.Incident, logItem.Title, logItem.Message);
        }

        public IExceptionLogItem CreateExceptionLogItem(IExceptionLogItem exceptionLogItem)
        {
            return m_mapper.CreateExceptionLogItem(exceptionLogItem.Category, exceptionLogItem.Event, exceptionLogItem.Severity, exceptionLogItem.Incident, exceptionLogItem.Exception);
        }

        #endregion

        #region ILogServiceContract Members

        LogIdDTO ILogServiceContract.FindLastLogId()
        {
            var result = FindLastLogId();

            return result.AutoConvert<ILogId, LogIdDTO>();
        }

        LogIdDTO ILogServiceContract.FindLastLogIdByIncidentId(Guid incidentId)
        {
            var result = FindLastLogIdByIncidentId(incidentId);

            return result.AutoConvert<ILogId, LogIdDTO>();
        }

        bool ILogServiceContract.IsLastLogId(LogIdDTO id)
        {
            return IsLastLogId(id);
        }

        bool ILogServiceContract.IsLastLogIdByIncidentId(Guid incidentId, LogIdDTO id)
        {
            return IsLastLogIdByIncidentId(incidentId, id);
        }

        List<SummaryLogItemDTO> ILogServiceContract.FindSummaryLogItemzByIncidentId(Guid incidentId)
        {
            var result = FindSummaryLogItemzByIncidentId(incidentId);

            return result.AutoConvertList<ISummaryLogItem, SummaryLogItemDTO>();
        }

        LogItemDTO ILogServiceContract.FindLogItem(LogIdDTO id)
        {
            var result = FindLogItem(id);

            return result.AutoConvert<ILogItem, LogItemDTO>();
        }

        LogItemDTO ILogServiceContract.CreateLogItem(LogItemDTO logItem)
        {
            var result = CreateLogItem(logItem.AutoConvert<LogItemDTO, LogItem>());

            return result.AutoConvert<ILogItem, LogItemDTO>();
        }

        ExceptionLogItemDTO ILogServiceContract.CreateExceptionLogItem(ExceptionLogItemDTO exceptionLogItem)
        {
            var result = CreateExceptionLogItem(exceptionLogItem.AutoConvert<ExceptionLogItemDTO, ExceptionLogItem>());

            return result.AutoConvert<IExceptionLogItem, ExceptionLogItemDTO>();
        }

        #endregion
    }
}
