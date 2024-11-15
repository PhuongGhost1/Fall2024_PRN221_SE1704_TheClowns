﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TicketResell_DAO;

namespace TIcketResell_Repository
{
    public class ChatRepository : IChatRepository
    {
        public async Task<bool> AddMessageAsync(Chat message) => await ChatDAO.GetInstance.AddMessageAsync(message);

        public async Task<Conversation> CreateConversationAsync(Guid buyerId, Guid sellerId, Guid ticketId) => await ChatDAO.GetInstance.CreateConversationAsync(buyerId, sellerId, ticketId);

        public async Task<bool> EndConversation(Guid? conversationId) => await ChatDAO.GetInstance.EndConversation(conversationId);

        public async Task<Conversation?> GetActiveConversationAsync(Guid buyerId, Guid sellerId, Guid ticketId) => await ChatDAO.GetInstance.GetActiveConversationAsync(buyerId, sellerId, ticketId);

        public async Task<List<Chat>> GetMessagesByConversationIdAsync(Guid conversationId) => await ChatDAO.GetInstance.GetMessagesByConversationIdAsync((Guid)conversationId);

        public async Task<List<Conversation>> LoadUserConversationsAsync(Guid? userId) => await ChatDAO.GetInstance.LoadUserConversationsAsync(userId);
    }
}
