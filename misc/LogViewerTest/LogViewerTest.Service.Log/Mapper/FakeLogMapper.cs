using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogViewerTest.Service.Log.Mapper
{
    internal class FakeLogMapper : ILogMapper
    {
        private static SynchronizedCollection<ILogItem> LogItemz = new SynchronizedCollection<ILogItem>();
        static FakeLogMapper()
        {
            Guid i1 = new Guid("{97F6056E-1A7E-4f0e-A0ED-895A556BBDC5}");
            Guid i2 = new Guid("{E5749F58-8E93-4f29-9143-22F5EFDD74BB}");

            LogItemz.Add(new LogItem() { Id = 1, Title = "Title1", Message = "la", Incident = i1 });
            LogItemz.Add(new LogItem() { Id = 2, Title = "Title2", Message = "la", Incident = i1 });
            LogItemz.Add(new LogItem() { Id = 3, Title = "Title3", Message = "la", Incident = i1 });
            LogItemz.Add(new LogItem() { Id = 4, Title = "Title4", Message = "la", Incident = i1 });
            LogItemz.Add(new LogItem() { Id = 5, Title = "Title5", Message = "la", Incident = i1 });
            LogItemz.Add(new LogItem() { Id = 6, Title = "Title6", Message = "la", Incident = i2 });
            LogItemz.Add(new LogItem() { Id = 7, Title = "Title7", Message = "la", Incident = i1 });
            LogItemz.Add(new LogItem() { Id = 8, Title = "Title8", Message = "la", Incident = i1 });
            LogItemz.Add(new LogItem() { Id = 9, Title = "Title9", Message = "la", Incident = i2 });
            LogItemz.Add(new LogItem() { Id = 10, Title = "Title10", Message = "la", Incident = i1 });
            LogItemz.Add(new LogItem() { Id = 11, Title = "Title11", Message = "la", Incident = i1 });
            LogItemz.Add(new LogItem() { Id = 12, Title = "Title12", Message = "la", Incident = i2 });
            LogItemz.Add(new LogItem() { Id = 13, Title = "Title13", Message = "la", Incident = i1 });
            LogItemz.Add(new LogItem() { Id = 14, Title = "Title14", Message = "la", Incident = i1 });
            LogItemz.Add(new LogItem() { Id = 15, Title = "Title15", Message = "la", Incident = i2 });
            LogItemz.Add(new LogItem() { Id = 16, Title = "Title16", Message = "la", Incident = i1 });
            LogItemz.Add(new LogItem() { Id = 17, Title = "Title17", Message = "la", Incident = i2 });
            LogItemz.Add(new LogItem() { Id = 18, Title = "Title18", Message = "la", Incident = i1 });
            LogItemz.Add(new LogItem() { Id = 19, Title = "Title19", Message = "la", Incident = i1 });
        }

        #region ILogMapper Members

        public long FindLastLogId()
        {
            return LogItemz.Max(x => x.Id);
        }

        public long FindLastLogIdByIncidentId(Guid incidentId)
        {
            return FindLogItemzByIncidentId(incidentId).Max(x => x.Id);
        }

        public bool IsLastLogId(long id)
        {
            return FindLastLogId() == id;
        }

        public bool IsLastLogIdByIncidentId(Guid incidentId, long id)
        {
            return FindLastLogIdByIncidentId(incidentId) == id;
        }

        private static Random m_random = new Random();

        public List<ILogItem> FindLogItemzByIncidentId(Guid incidentId)
        {
            var itemz = (from s in LogItemz
                         where s.Incident == incidentId
                         select s).ToList();

            if (itemz.Count < 100)
            {
                int generateCount = m_random.Next(0, 10);
                for (int i = 0; i < generateCount; i++)
                {
                    ILogItem item = GenerateNextLogItem(incidentId);
                    itemz.Add(item);
                }
            }

            return itemz;
        }

        private ILogItem GenerateNextLogItem(Guid incidentId)
        {
            LogItem item = new LogItem();
            item.Id = 0;
            item.Message = "Logitem " + m_random.Next().ToString();
            item.Title = "Logitem " + m_random.Next().ToString();
            item.Incident = incidentId;

            while (FindLastLogId() >= item.Id)
            {
                item.Id = GenerateNextLogId();
            }
            LogItemz.Add(item);

            return item;
        }

        private long GenerateNextLogId()
        {
            return FindLastLogId() + 1;
        }

        public ILogItem FindLogItem(long id)
        {
            return (from s in LogItemz
                    where s.Id == id
                    select s).FirstOrDefault();
        }

        public LogItem CreateLogItem(Guid categoryGuid, Guid eventGuid, Guid severityGuid, Guid incident, string title, string message)
        {
            LogItem newLogItem = new LogItem()
            {
                Id = FindLastLogId(),
                Category = categoryGuid,
                Event = eventGuid,
                Severity = severityGuid,
                Incident = incident,
                Title = title,
                Message = message
            };

            LogItemz.Add(newLogItem);

            return newLogItem;
        }

        public ExceptionLogItem CreateExceptionLogItem(Guid categoryGuid, Guid eventGuid, Guid severityGuid, Guid incident, Exception exception)
        {
            ExceptionLogItem newExceptionLogItem = new ExceptionLogItem()
            {
                Id = FindLastLogId(),
                Category = categoryGuid,
                Event = eventGuid,
                Severity = severityGuid,
                Incident = incident,
                Exception = exception
            };

            LogItemz.Add(newExceptionLogItem);

            return newExceptionLogItem;
        }

        #endregion
    }
}
