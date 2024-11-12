using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;

namespace TIcketResell_Repository
{
    public interface IChatRepository
    {
        Task<bool> AddMessageAsync(Chat message);
        Task<List<Conversation>> LoadUserConversationsAsync(Guid? userId);
        Task<Conversation> CreateConversationAsync(Guid buyerId, Guid sellerId, Guid ticketId);
        Task<List<Chat>> GetMessagesByConversationIdAsync(Guid conversationId);
        Task<Conversation?> GetActiveConversationAsync(Guid buyerId, Guid sellerId, Guid ticketId);
        Task<bool> EndConversation(Guid? conversationId);
    }
}
