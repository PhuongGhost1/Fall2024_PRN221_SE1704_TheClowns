using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;

namespace TicketResell_Service
{
    public interface ITransactionService
    {
        Task<Transactions> GetTransactionByIdAsync(Guid id);
        Task<List<Transactions>> GetTransactionsByTicketIdAsync(Guid ticketId);
        Task<List<Transactions>> GetTransactionsByUserIdAsync(Guid? userId);
        Task<bool> SaveDepositeTransaction(Guid userId, long amount);
        Task<string> CreateTransactionAsync(Guid conversationId, Guid buyerId, Guid sellerId, Guid ticketId, decimal amount);
        Task<Transactions> GetByPayPalPaymentIdAsync(string paymentId);
        Task<bool> UpdateTransactionAsync(Transactions transactions);
        Task<bool> GetMoneyWalletFromBuyerAsync(Guid? userId, decimal price);
        Task<Transactions?> GetTransactionBySomeIdsAsync(Guid? buyerId, Guid? sellerId, Guid? ticketId);
        Task<int> CheckBookingPunishmentAsync(Guid? userId, string status);
        Task<int> CheckBookingPunishmentForPendingAsync(Guid? userId);
        Task<bool> CheckIsTransactionCompletedAsync(Guid? conversationId);
        Task<List<Transactions>> GetTransactions();
        Task<int> GetCountTransactions();
    }
}
