using System.Runtime.Serialization;

namespace LogViewerTest.Service.Log.DTO
{
    [DataContract]
    public class LogIdDTO : ILogId
    {
        [DataMember]
        public long Id { get; set; }
    }
}
