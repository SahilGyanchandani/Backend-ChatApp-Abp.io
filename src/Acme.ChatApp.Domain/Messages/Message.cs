using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey(nameof(SenderId))]
        public Guid SenderId { get; set; } //SenderID Of User
        public IdentityUser? Sender { get; set; }
        [ForeignKey(nameof(SenderId))]
        public Guid ReceiverId { get; set; } //ReceiverID of User
        public IdentityUser? Receiver { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }   

    }
}
