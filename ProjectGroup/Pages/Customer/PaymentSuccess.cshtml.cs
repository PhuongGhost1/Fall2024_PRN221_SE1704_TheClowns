using Azure.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PayPal;
using PayPal.Api;
using TicketResell_BusinessObject;
using TicketResell_Service;

namespace ProjectGroup.Pages.Customer
{
    public class PaymentSuccessModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ITransactionService _transactionService;
        private readonly ITicketService _ticketService;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        public Guid? UserId { get; private set; }
        public Guid? RoleId { get; private set; }
        public PaymentSuccessModel(IConfiguration configuration, ITransactionService transactionService, ITicketService ticketService,
                                    IHttpContextAccessor httpContextAccessor, IChatService chatService, IUserService userService)
        {
            _configuration = configuration;
            _transactionService = transactionService;
            _ticketService = ticketService;
            _chatService = chatService;
            _userService = userService;

            string userIdString = httpContextAccessor.HttpContext.Session.GetString("UserId");
            string roleIdString = httpContextAccessor.HttpContext.Session.GetString("RoleId");

            UserId = string.IsNullOrEmpty(userIdString) ? (Guid?)null : Guid.Parse(userIdString);
            RoleId = string.IsNullOrEmpty(roleIdString) ? (Guid?)null : Guid.Parse(roleIdString);

        }
        public async Task<IActionResult> OnGetAsync(string paymentId, string payerId)
        {
            if (UserId != null && RoleId != null)
            {
                if (RoleId != Guid.Parse("57FA54D1-7C67-4405-B277-B8A58049FF3C"))
                {
                    return RedirectToPage("/TicketsPage/Index");
                }
            }
            else
            {
                return RedirectToPage("/Authentication/Login");
            }

            if (string.IsNullOrEmpty(paymentId) || string.IsNullOrEmpty(payerId))
            {
                TempData["Error"] = "Payment information is missing or invalid.";
                return Page();
            }

            var transaction = await _transactionService.GetByPayPalPaymentIdAsync(paymentId);
            if (transaction == null || transaction.TransactionStatus == "Completed")
            {
                TempData["Error"] = transaction == null ? "Transaction not found." : "Transaction already completed.";
                return Page();
            }

            try
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

                var payment = Payment.Get(apiContext, paymentId);
                if (payment.state.ToLower() == "created")
                {
                    var paymentExecution = new PaymentExecution() { payer_id = payerId };
                    payment = payment.Execute(apiContext, paymentExecution);
                }

                if (payment.state.ToLower() != "approved")
                {
                    TempData["Error"] = "Payment was not approved.";
                    return Page();
                }

                transaction.TransactionStatus = "Completed";
                await _transactionService.UpdateTransactionAsync(transaction);
                var ticket = await _ticketService.GetTicketByIdAsync(transaction.TicketId);
                if (ticket != null)
                {
                    ticket.TicketStatus = "Sold";
                    await _ticketService.UpdateTicketAsync(ticket);
                }

                var user = await _userService.GetUserByIdAsync(UserId);

                if (user != null)
                {
                    int totalReputationAdjustment = 0;             

                    int reputationPointsComplete = await _transactionService.CheckBookingPunishmentAsync(UserId, "Completed");
                    if (reputationPointsComplete > 0)
                    {
                        totalReputationAdjustment += 5;
                    }

                    user.ReputationPoints += totalReputationAdjustment;
                    await _userService.UpdateUserAsync(user);
                }

                var conversation = await _chatService.GetActiveConversationAsync(transaction.BuyerId, transaction.SellerId, transaction.TicketId);
                if (conversation != null)
                {
                    var chat = new Chat
                    {
                        Id = Guid.NewGuid(),
                        ConversationId = conversation.Id,
                        BuyerId = null,
                        SellerId = UserId,
                        Message = "Transaction completed successfully! Thank you for your support!",
                        SentAt = DateTime.UtcNow,
                        IsSender = true
                    };

                    await _chatService.SendMessageAsync(chat);
                }

                TempData["SuccessMessage"] = "Transaction completed successfully!";
                return Page();
            }
            catch (PaymentsException ex)
            {
                TempData["Error"] = "Error processing payment: " + ex.Message;
                return Page();
            }
        }
    }
}
