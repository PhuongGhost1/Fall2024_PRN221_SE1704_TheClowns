using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;

namespace TicketResell_Service
{
    public interface IChatService
    {
        Task<bool> SendMessageAsync(Chat chat);
        Task<List<Conversation>> LoadUserConversationsAsync(Guid? userId);
        Task<Conversation> CreateConversationAsync(Guid buyerId, Guid sellerId, Guid ticketId);
        Task<List<Chat>> GetMessagesByConversationIdAsync(Guid conversationId);
        Task<Conversation?> GetActiveConversationAsync(Guid buyerId, Guid sellerId, Guid ticketId);
        Task<bool> EndConversationAsync(Guid? conversationId);
    }
}
