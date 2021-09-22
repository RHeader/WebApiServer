using System;

namespace Client
{
    class MessageModel
    {
        public Guid id { get; set; }
        public string message { get; set; }
        public string userName { get; set; }
        public string messageSendTime { get; set; }
    }
}
