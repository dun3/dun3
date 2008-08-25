using System;

namespace LogViewerTest.Service.Log
{
    internal class LogItem : ILogItem, ISummaryLogItem
    {
        public long Id { get; set; }

        public Guid Category { get; set; }
        public Guid Event { get; set; }
        public Guid Severity { get; set; }
        public Guid Incident { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Summary
        {
            get
            {
                return Title;
            }
        }
    }
}
