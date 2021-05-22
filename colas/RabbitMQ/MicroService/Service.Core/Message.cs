using System;

namespace Service.Core
{
    public class Message
    {
        public Message()
        {
            Id = Guid.NewGuid();            
        }

        public Guid Id { get; }
        public DateTime Date { get; set; }
        public string Subject { get; set;  }
        public string Text { get; set; }
    }
}
