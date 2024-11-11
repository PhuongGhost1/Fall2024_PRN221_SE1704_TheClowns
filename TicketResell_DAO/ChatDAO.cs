using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TicketResell_BusinessObject;

namespace TicketResell_DAO
{
    public class ChatDAO
    {
        private readonly TicketResellContext _context;
        public static ChatDAO _instance;

        public ChatDAO()
        {
            _context = new TicketResellContext();
        }

        public static ChatDAO GetInstance
        {
            get{
                if (_instance == null)
                {
                    _instance = new ChatDAO();
                }
                return _instance;
            }
        }

        public async Task<Conversation> CreateConversationAsync(Guid buyerId, Guid sellerId, Guid ticketId)
        {
            var newConversation = new Conversation
            {
                Id = Guid.NewGuid(),
                BuyerId = buyerId,
                SellerId = sellerId,
                TicketId = ticketId,
                StartedAt = DateTime.UtcNow
            };

            _context.Conversations.Add(newConversation);
            await _context.SaveChangesAsync();
            return newConversation;
        }

        public async Task<Conversation?> GetActiveConversationAsync(Guid buyerId, Guid sellerId, Guid ticketId)
        {
            return await _context.Conversations
                .Include(c => c.Ticket)
                .Include(c => c.Buyer)
                .Include (c => c.Seller)
                .FirstOrDefaultAsync(c => c.BuyerId == buyerId && c.SellerId == sellerId && c.TicketId == ticketId);
        }

        public async Task<bool> EndConversation(Guid? conversationId)
        {
            var conversation = await _context.Conversations.FirstOrDefaultAsync(c => c.Id == conversationId);

            if (conversation == null) return false;

            conversation.EndedAt = DateTime.UtcNow;
            _context.Conversations.Update(conversation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddMessageAsync(Chat message)
        {
            _context.Chats.Add(message);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Chat>> GetMessagesByConversationIdAsync(Guid conversationId)
        {
            return await _context.Chats
                .Where(c => c.ConversationId == conversationId)
                .OrderBy(c => c.SentAt)
                .ToListAsync();
        }

        public async Task<List<Conversation>> LoadUserConversationsAsync(Guid? userId)
        {
            return await _context.Conversations
                .Where(c => c.BuyerId == userId || c.SellerId == userId)
                .Include(c => c.Buyer)
                .Include(c => c.Seller)
                .Include(c => c.Ticket)
                .ToListAsync();
        }
    }
}