using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogViewerTest.Service.Log
{
    public class SummaryLogItem : ISummaryLogItem
    {
        public long Id { get; set; }
        public Guid Incident { get; set; }
        public string Summary { get; set; }
    }
}
