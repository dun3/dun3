using System;
using System.Runtime.Serialization;

namespace LogViewerTest.Service.Log.DTO
{
    [DataContract]
    public class LogItemDTO : ILogItem
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public Guid Category { get; set; }

        [DataMember]
        public Guid Event { get; set; }

        [DataMember]
        public Guid Severity { get; set; }

        [DataMember]
        public Guid Incident { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
