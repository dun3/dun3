using System.Runtime.Serialization;
using System;

namespace LogViewerTest.Service.Log.DTO
{
    [DataContract]
    public class SummaryLogItemDTO : ISummaryLogItem
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Summary { get; set; }

        [DataMember]
        public Guid Incident { get; set; }
    }
}
