﻿
using Acme.ChatApp.MessagesDto;
using Acme.ChatApp.RedisConn;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Users;

namespace Acme.ChatApp.Hubs
{
    [HubRoute("/chat")]
    public class ChatHub:AbpHub
    {
        private readonly IRedisConnection _connection;

        public ChatHub(IRedisConnection connection)
        {
            _connection = connection;
        }

        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var userId = CurrentUser.GetId();

            await _connection.AddConnectionAsync(Convert.ToString(userId), connectionId);

            await base.OnConnectedAsync();
        }

        public async Task SendMessage(SendMessageDto msg)
        {
            var userId = Context.UserIdentifier;

            var receiverConnectionId = await _connection.GenConnIdAsync(Convert.ToString(msg.ReceiverId));

            await Clients.Client(receiverConnectionId).SendAsync("Broadcast", msg, userId);
        }
    }
}
