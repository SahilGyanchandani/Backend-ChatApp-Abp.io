using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.ChatApp.Messages
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public Guid SenderId { get; set; } //SenderID Of User
        public Guid ReceiverId { get; set; } //ReceiverID of User
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
