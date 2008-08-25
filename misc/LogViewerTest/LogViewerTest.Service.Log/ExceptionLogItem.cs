using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogViewerTest.Service.Log
{
    internal class ExceptionLogItem : IExceptionLogItem, ISummaryLogItem
    {
        public long Id { get; set; }
        public Guid Category { get; set; }
        public Guid Event { get; set; }
        public Guid Severity { get; set; }
        public Guid Incident { get; set; }

        public string Title
        {
            get
            {
                if (Exception != null)
                {
                    return "Exception: " + Exception.GetType().Name;
                }
                else
                {
                    return "(null)";
                }
            }
        }

        public string Message
        {
            get
            {
                if (Exception != null)
                {
                    return Exception.ToString();
                }
                else
                {
                    return "(null)";
                }
            }
        }

        public Exception Exception { get; set; }

        public string Summary
        {
            get { return Title; }
        }
    }
}
