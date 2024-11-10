using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketResell_BusinessObject;

namespace TicketResell_DAO
{
    public class TransactionDAO
    {
        private TicketResellContext _context;
        private static TransactionDAO _instance;

        public TransactionDAO()
        {
            _context = new TicketResellContext();
        }

        public static TransactionDAO getInstance
        {
            get{ 
                if(_instance == null)
                {
                    _instance = new TransactionDAO();
                }
                return _instance;
            }
        }

        public async Task<bool> CreateTransaction(Transactions transaction)
        {
            bool flag = true;
            try
            {
                await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                flag = false;
                var innerException = ex.InnerException?.Message;
                Console.WriteLine("Error Details: " + innerException);
            }
            return flag;
        }

        public async Task<Transactions> GetTransactionByID(Guid transactionID)
        {
            return await _context.Transactions.FirstOrDefaultAsync(t => t.Id == transactionID);
        }

        public async Task<Transactions> GetByPayPalPaymentId(string paymentId)
        {
            return await _context.Transactions.FirstOrDefaultAsync(t => t.PaypalPaymentId == paymentId);
        }

        public async Task<List<Transactions>> GetTransactionsByUserID(Guid? userID)
        {
            return await _context.Transactions.Where(t => (t.SellerId == userID ? t.SellerId : t.BuyerId) == userID).ToListAsync();
        }

        public async Task<bool> UpdateTransaction(Transactions transaction)
        {
            bool flag = true;
            try
            {
                _context.Transactions.Update(transaction);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        public async Task<List<Transactions>> GetByTicketIdAsync(Guid ticketId)
        {
            return await _context.Transactions.Where(t => t.TicketId == ticketId).ToListAsync();
        }

        public async Task<bool> GetMoneyToWalletById(Guid? userId, decimal amount)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            user.Wallet += amount;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Transactions?> GetTransactionBySomeIds(Guid? buyerId, Guid? sellerId, Guid? ticketId)
        {
            return await _context.Transactions
                .Where(t => t.BuyerId == buyerId && t.SellerId == sellerId && t.TicketId == ticketId)
                .FirstOrDefaultAsync();
        }

        public async Task<int> CheckBookingPunishment(Guid? userId, string status)
        {
            return await _context.Transactions.CountAsync(t => t.BuyerId == userId && t.TransactionStatus == status);
        }

        public async Task<int> CheckBookingPunishmentForPending(Guid? userId)
        {
            var pendingTransactions = await _context.Transactions
                .Where(t => t.BuyerId == userId && t.TransactionStatus == "Pending")
                .Include(t => t.Ticket)
                .ToListAsync();

            int delayedPendingCount = pendingTransactions
                .Count(t => t.Ticket.ExpirationDate.HasValue
                            && t.TransactionDate.HasValue
                            && (t.Ticket.ExpirationDate.Value - t.TransactionDate.Value).TotalDays > 1.5f);

            return delayedPendingCount;
        }

        public async Task<bool> CheckIsTransactionCompleted(Guid? conversationId)
        {
            return await _context.Transactions.AnyAsync(t => t.Ticket.Conversations.Any(c => c.Id == conversationId) && 
                                        t.TransactionStatus == "Completed");
        }
    }
}
