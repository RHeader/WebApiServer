using System;

namespace WebApiServer.Models
{
    public class Message
    {
        public Guid id { get; set; }
        public string message { get; set; }

        public string userName { get; set; }

        public string messageSendTime { get; set; }
    }
}
