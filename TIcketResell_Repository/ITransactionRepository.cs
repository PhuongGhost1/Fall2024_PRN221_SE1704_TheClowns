using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;

namespace TIcketResell_Repository
{
    public interface ITransactionRepository
    {
        Task<Transactions> GetByIdAsync(Guid id);
        Task<List<Transactions>> GetByTicketIdAsync(Guid ticketId);
        Task<bool> AddAsync(Transactions transaction);
        Task<bool> UpdateAsync(Transactions transaction);
        Task<List<Transactions>> GetByUserIdAsync(Guid? userId);
        Task<Transactions> GetByPayPalPaymentId(string paymentId);
        Task<bool> GetMoneyWalletFromBuyer(Guid? userId, decimal price);
        Task<Transactions?> GetTransactionBySomeIds(Guid? buyerId, Guid? sellerId, Guid? ticketId);
        Task<int> CheckBookingPunishment(Guid? userId, string status);
        Task<int> CheckBookingPunishmentForPending(Guid? userId);
        Task<bool> CheckIsTransactionCompleted(Guid? conversationId);
        Task<List<Transactions>> TransactionsSortedByDate();
        Task<int> CountAsync();
    }
}
