using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TicketResell_DAO;

namespace TIcketResell_Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        public async Task<bool> AddAsync(Transactions transaction) => await TransactionDAO.getInstance.CreateTransaction(transaction);

        public async Task<int> CheckBookingPunishment(Guid? userId, string status) => await TransactionDAO.getInstance.CheckBookingPunishment(userId, status);

        public async Task<int> CheckBookingPunishmentForPending(Guid? userId) => await TransactionDAO.getInstance.CheckBookingPunishmentForPending(userId);

        public async Task<bool> CheckIsTransactionCompleted(Guid? conversationId) => await TransactionDAO.getInstance.CheckIsTransactionCompleted(conversationId);

        public async Task<int> CountAsync() => await TransactionDAO.getInstance.CountTrsactions();

        public async Task<Transactions> GetByIdAsync(Guid id) => await TransactionDAO.getInstance.GetTransactionByID(id);

        public async Task<Transactions> GetByPayPalPaymentId(string paymentId) => await TransactionDAO.getInstance.GetByPayPalPaymentId(paymentId);

        public async Task<List<Transactions>> GetByTicketIdAsync(Guid ticketId) => await TransactionDAO.getInstance.GetByTicketIdAsync(ticketId);

        public async Task<List<Transactions>> GetByUserIdAsync(Guid? userId) => await TransactionDAO.getInstance.GetTransactionsByUserID(userId);

        public async Task<bool> GetMoneyWalletFromBuyer(Guid? userId, decimal price) => await TransactionDAO.getInstance.GetMoneyToWalletById(userId, price);

        public async Task<Transactions?> GetTransactionBySomeIds(Guid? buyerId, Guid? sellerId, Guid? ticketId) => await TransactionDAO.getInstance.GetTransactionBySomeIds(buyerId, sellerId, ticketId);

        public async Task<List<Transactions>> TransactionsSortedByDate() => await TransactionDAO.getInstance.GetAllTransactionsSortedByDate();

        public async Task<bool> UpdateAsync(Transactions transaction) => await TransactionDAO.getInstance.UpdateTransaction(transaction);
    }
}
