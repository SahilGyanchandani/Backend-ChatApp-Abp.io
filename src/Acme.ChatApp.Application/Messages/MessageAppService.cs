using Acme.ChatApp.EntityFrameworkCore;
using Acme.ChatApp.MessagesDto;
using Acme.ChatApp.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;

namespace Acme.ChatApp.Messages
{
    [Authorize]
    public class MessageAppService : ApplicationService
    {
        private readonly ICurrentUser _currentUser;
        private readonly ChatAppDbContext _context;
        public MessageAppService(ICurrentUser currentUser, ChatAppDbContext dbContext)
        {
            _currentUser = currentUser;
            _context = dbContext;
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

            // Search for messages that contain the provided keywords in the conversation of the current user
            var conversations = await _context.Messages
                .Where(m => (m.SenderId == currentUser || m.ReceiverId == currentUser) &&
                            EF.Functions.Like(m.Content, $"%{query}%")) // Using EF.Functions.Like for case-insensitive comparison
                .OrderBy(m => m.Timestamp)
                .ToListAsync();

            var response = conversations.Select(m => new MessageResponse
            {
                MessageId = m.MessageId,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                Content = m.Content,
                Timestamp = m.Timestamp
            });
            return response;
        }
        public async Task<bool> UpdateMessage(int msgId,string Content)
        {
            var CurrentUser = _currentUser.GetId();
            var messageEdit = await _context.Messages.FindAsync(msgId);
            if(messageEdit != null )
            {
                messageEdit.Content = Content;
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
           ;
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
