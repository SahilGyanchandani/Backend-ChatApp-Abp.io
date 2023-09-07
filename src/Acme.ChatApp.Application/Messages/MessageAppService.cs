using Acme.ChatApp.EntityFrameworkCore;
using Acme.ChatApp.Hubs;
using Acme.ChatApp.Messages;
using Acme.ChatApp.MessagesDto;
using Acme.ChatApp.RedisConn;
using Acme.ChatApp.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Users;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Acme.ChatApp.Messages
{
    [Authorize]
    public class MessageAppService : ApplicationService
    {
        private readonly ICurrentUser _currentUser;
        private readonly ChatAppDbContext _context;
        private readonly IRedisConnection _connection;

        public MessageAppService(ICurrentUser currentUser, ChatAppDbContext dbContext, IRedisConnection connection)
        {
            _currentUser = currentUser;
            _context = dbContext;
            _connection = connection;
           
        }
        public async Task<MessageResponse> CreateAsync(SendMessageDto msg)
        {
            var CurrentUser = _currentUser.GetId();
            var message = new Message
            {
                SenderId = CurrentUser,
                ReceiverId = msg.ReceiverId,
                Content = msg.Content,
                Timestamp = DateTime.Now
            };
            var result = await _context.Messages.AddAsync(message);
            var saveResult = await _context.SaveChangesAsync();

            var response = new MessageResponse
            {
                MessageId = message.MessageId,
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                Content = message.Content,
                Timestamp = DateTime.Now
            };
            return response;
        }
        public async Task<IEnumerable<MessageResponse>> GetConversationHistory(Guid receiverId, DateTime? before = null, int count = 20, string sort = "asc")
        {
            var CurrentUser = _currentUser.GetId();

            bool isAscending = sort.ToLower() == "asc";

            var messages = await _context.Messages
               .Where(m => m.SenderId == CurrentUser && m.ReceiverId == receiverId || m.SenderId == receiverId && m.ReceiverId == CurrentUser)
               .Where(m => before == null || (isAscending ? m.Timestamp < before : m.Timestamp > before))
               .OrderByDescending(m => m.Timestamp)
               .Take(count)
               .ToListAsync();

            if (isAscending)
            {
                messages.Reverse();
            }

            var response = messages.Select(m => new MessageResponse
            {
                MessageId = m.MessageId,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                Content = m.Content,
                Timestamp = m.Timestamp
            }).ToList();

            return response;
        }

        public async Task<IEnumerable<MessageResponse>> GetSearchConversation(string query)
        {
            var currentUser = _currentUser.GetId();

            var keywords = query.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //Fetch all messages of the current user
               var users = await _context.Messages
                   .Where(m => (m.SenderId == currentUser || m.ReceiverId == currentUser))
                   .ToListAsync();

            // Filter messages containing the provided keywords
            var conversation = users
                .Where(m => keywords.Any(keyword => m.Content.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
                .OrderBy(m => m.Timestamp)
                .ToList();


            var response = conversation.Select(m => new MessageResponse
            {
                MessageId = m.MessageId,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                Content = m.Content,
                Timestamp = m.Timestamp
            });
            return response;
        }

        public async Task<Message> UpdateMessage(int msgId,string Content)
        {
            var CurrentUser = _currentUser.GetId();
            var messageEdit = await _context.Messages.FindAsync(msgId);
            if(messageEdit != null )
            {
                messageEdit.Content = Content;
                await _context.SaveChangesAsync();
                return messageEdit;
            }
            else
            {
                throw new UserFriendlyException("Message not found");
            }
        }
        public async Task<bool> DeleteMessage(int msgId)
        {
            var CurrentUser = _currentUser.GetId();

            var messageDelete = await _context.Messages.FindAsync(msgId);
            if (messageDelete != null)
            {
                _context.Messages.Remove(messageDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
