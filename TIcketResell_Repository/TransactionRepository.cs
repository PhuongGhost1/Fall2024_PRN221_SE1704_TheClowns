using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TicketResell_DAO;

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
    }
    public class TransactionRepository : ITransactionRepository
    {
        public async Task<bool> AddAsync(Transactions transaction) => await TransactionDAO.getInstance.CreateTransaction(transaction);

        public async Task<Transactions> GetByIdAsync(Guid id) => await TransactionDAO.getInstance.GetTransactionByID(id);

        public async Task<Transactions> GetByPayPalPaymentId(string paymentId) => await TransactionDAO.getInstance.GetByPayPalPaymentId(paymentId);

        public async Task<List<Transactions>> GetByTicketIdAsync(Guid ticketId) => await TransactionDAO.getInstance.GetByTicketIdAsync(ticketId);

        public async Task<List<Transactions>> GetByUserIdAsync(Guid? userId) => await TransactionDAO.getInstance.GetTransactionsByUserID(userId);

        public async Task<bool> GetMoneyWalletFromBuyer(Guid? userId, decimal price) => await TransactionDAO.getInstance.GetMoneyToWalletById(userId, price);

        public async Task<bool> UpdateAsync(Transactions transaction) => await TransactionDAO.getInstance.UpdateTransaction(transaction);
    }
}
