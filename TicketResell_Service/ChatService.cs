using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TIcketResell_Repository;

namespace TicketResell_Service
{
    public interface IChatService
    {
        Task<bool> SendMessageAsync(Chat chat);
        Task<List<Conversation>> LoadUserConversationsAsync(Guid? userId);
        Task<Conversation> CreateConversationAsync(Guid buyerId, Guid sellerId, Guid ticketId);
        Task<List<Chat>> GetMessagesByConversationIdAsync(Guid conversationId);
        Task<Conversation?> GetActiveConversationAsync(Guid buyerId, Guid sellerId, Guid ticketId);
    }
    
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<List<Conversation>> LoadUserConversationsAsync(Guid? userId) => await _chatRepository.LoadUserConversationsAsync(userId);

        public async Task<bool> SendMessageAsync(Chat chat) => await _chatRepository.AddMessageAsync(chat);

        public async Task<Conversation> CreateConversationAsync(Guid buyerId, Guid sellerId, Guid ticketId)
        {
            return await _chatRepository.CreateConversationAsync(buyerId, sellerId, ticketId);
        }

        public async Task<List<Chat>> GetMessagesByConversationIdAsync(Guid conversationId)
        {
            return await _chatRepository.GetMessagesByConversationIdAsync(conversationId);
        }

        public async Task<Conversation?> GetActiveConversationAsync(Guid buyerId, Guid sellerId, Guid ticketId)
        {
            return await _chatRepository.GetActiveConversationAsync(buyerId, sellerId, ticketId);
        }

    }
}
