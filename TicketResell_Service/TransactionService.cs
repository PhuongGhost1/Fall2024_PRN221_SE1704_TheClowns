using Microsoft.Extensions.Configuration;
using PayPal;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TIcketResell_Repository;

namespace TicketResell_Service
{
    public interface ITransactionService
    {
        Task<Transactions> GetTransactionByIdAsync(Guid id);
        Task<List<Transactions>> GetTransactionsByTicketIdAsync(Guid ticketId);
        Task<List<Transactions>> GetTransactionsByUserIdAsync(Guid? userId);
        Task<bool> SaveDepositeTransaction(Guid userId, long amount);
        Task<string> CreateTransactionAsync(Guid conversationId, Guid buyerId, Guid sellerId, decimal amount);
        Task<Transactions> GetByPayPalPaymentIdAsync(string paymentId);
        Task<bool> UpdateTransactionAsync(Transactions transactions);
        Task<bool> GetMoneyWalletFromBuyerAsync(Guid? userId, decimal price);
    }

    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IConfiguration _configuration;
        public TransactionService(ITransactionRepository transactionRepository, IConfiguration configuration)
        {
            _transactionRepository = transactionRepository;
            _configuration = configuration;
        }

        public async Task<Transactions> GetTransactionByIdAsync(Guid id) => await _transactionRepository.GetByIdAsync(id);

        public async Task<List<Transactions>> GetTransactionsByTicketIdAsync(Guid ticketId) => await _transactionRepository.GetByTicketIdAsync(ticketId);

        public async Task<List<Transactions>> GetTransactionsByUserIdAsync(Guid? userId) => await _transactionRepository.GetByUserIdAsync(userId);

        public async Task<bool> SaveDepositeTransaction(Guid userId, long amount)
        {
            Transactions transaction = new Transactions();
            transaction.Id = Guid.NewGuid();
            if (transaction.SellerId == null)
            {
                transaction.SellerId = userId;
            }
            else
            {
                transaction.BuyerId = userId;
            }

            transaction.Amount = amount;    
            transaction.TransactionDate = DateTime.Now;   
            transaction.TransactionStatus = "Depoiste";
            return await _transactionRepository.AddAsync(transaction);
        }

        public async Task<string> CreateTransactionAsync(Guid conversationId, Guid buyerId, Guid sellerId, decimal amount)
        {
            var transaction = new Transactions
            {
                Id = Guid.NewGuid(),
                BuyerId = buyerId,
                SellerId = sellerId,
                Amount = amount,
                TransactionStatus = "Pending"
            };

            var success = await _transactionRepository.AddAsync(transaction);
            if (!success)
            {
                return null;
            }

            var approvalUrl = await CreatePayPalPayment(transaction);
            transaction.PaypalPaymentId = approvalUrl;
            await _transactionRepository.UpdateAsync(transaction); 

            return approvalUrl;
        }

        private async Task<string> CreatePayPalPayment(Transactions transaction)
        {
            var config = new Dictionary<string, string>
            {
                { "mode", _configuration["PayPal:Mode"] },
                { "clientId", _configuration["PayPal:ClientId"] },
                { "clientSecret", _configuration["PayPal:ClientSecret"] }
            };

            var apiContext = new PayPal.Api.APIContext(new PayPal.Api.OAuthTokenCredential(config["clientId"], config["clientSecret"]).GetAccessToken())
            {
                Config = config
            };

            var payment = new Payment
            {
                intent = "sale",
                payer = new Payer { payment_method = "paypal" },
                transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        amount = new Amount
                        {
                            total = ((decimal)transaction.Amount).ToString("F2"),
                            currency = "USD"
                        },
                        description = "Transaction description"
                    }
                },
                redirect_urls = new RedirectUrls
                {
                    cancel_url = _configuration["PayPal:FailUrl"],
                    return_url = _configuration["PayPal:SuccessUrl"]
                }
            };

            try
            {
                var createdPayment = payment.Create(apiContext);
                transaction.PaypalPaymentId = createdPayment.id;
                await _transactionRepository.UpdateAsync(transaction);
                return createdPayment.links.First(l => l.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase)).href;
            }
            catch (PaymentsException ex)
            {
                throw new Exception("Error creating PayPal payment: " + ex.Message, ex);
            }
        }

        public async Task<Transactions> GetByPayPalPaymentIdAsync(string paymentId) => await _transactionRepository.GetByPayPalPaymentId(paymentId);

        public async Task<bool> UpdateTransactionAsync(Transactions transactions) => await _transactionRepository.UpdateAsync(transactions);

        public async Task<bool> GetMoneyWalletFromBuyerAsync(Guid? userId, decimal price) => await _transactionRepository.GetMoneyWalletFromBuyer(userId, price);
    }
}
