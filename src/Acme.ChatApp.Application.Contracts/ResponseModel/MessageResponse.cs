using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.ChatApp.ResponseModel
{
    public class MessageResponse
    {
        public int MessageId { get; set; }
        public Guid SenderId { get; set; } //SenderID Of User
        public Guid ReceiverId { get; set; } //ReceiverID of User
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
