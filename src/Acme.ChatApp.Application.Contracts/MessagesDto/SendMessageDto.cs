using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.ChatApp.MessagesDto
{
    public class SendMessageDto
    {
        public Guid ReceiverId { get; set; }
        public string Content { get; set; }
    }
}
