using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogViewerTest.Service.Log.Mapper
{
    internal class DbLogMapper : ILogMapper
    {
        #region ILogMapper Members

        public long FindLastLogId()
        {
            throw new NotImplementedException();
        }

        public long FindLastLogIdByIncidentId(Guid incidentId)
        {
            throw new NotImplementedException();
        }

        public bool IsLastLogId(long id)
        {
            throw new NotImplementedException();
        }

        public bool IsLastLogIdByIncidentId(Guid incidentId, long id)
        {
            throw new NotImplementedException();
        }

        public List<ILogItem> FindLogItemzByIncidentId(Guid incidentId)
        {
            throw new NotImplementedException();
        }

        public ILogItem FindLogItem(long id)
        {
            throw new NotImplementedException();
        }

        public LogItem CreateLogItem(Guid categoryGuid, Guid eventGuid, Guid severityGuid, Guid incident, string title, string message)
        {
            throw new NotImplementedException();
        }

        public ExceptionLogItem CreateExceptionLogItem(Guid categoryGuid, Guid eventGuid, Guid severityGuid, Guid incident, Exception exception)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
