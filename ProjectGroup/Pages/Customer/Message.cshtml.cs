using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PayPal;
using PayPal.Api;
using System;
using TicketResell_BusinessObject;
using TicketResell_Service;

namespace ProjectGroup.Pages.Customer
{
    public class MessageModel : PageModel
    {
        private readonly IChatService _chatService;
        private readonly ITransactionService _transactionService;
        private readonly ITicketService _ticketService;
        private readonly IConfiguration _configuration;

        public Guid? UserId { get; private set; }
        public Guid? RoleId { get; private set; }
        public List<Conversation> Conversations { get; set; } = new List<Conversation>();
        public Conversation? CurrentConversation { get; set; }
        public List<Chat> Messages { get; set; } = new List<Chat>();

        public MessageModel(IChatService chatService, IHttpContextAccessor httpContextAccessor, ITransactionService transactionService,
                            ITicketService ticketService, IConfiguration configuration)
        {
            _chatService = chatService;
            string userIdString = httpContextAccessor.HttpContext.Session.GetString("UserId");
            string roleIdString = httpContextAccessor.HttpContext.Session.GetString("RoleId");

            UserId = string.IsNullOrEmpty(userIdString) ? (Guid?)null : Guid.Parse(userIdString);
            RoleId = string.IsNullOrEmpty(roleIdString) ? (Guid?)null : Guid.Parse(roleIdString);
            _transactionService = transactionService;
            _ticketService = ticketService;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync(Guid? buyerId, Guid? sellerId, Guid? ticketId)
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

            Conversations = await _chatService.LoadUserConversationsAsync(UserId);

            if (buyerId.HasValue && sellerId.HasValue && ticketId.HasValue)
            {
                CurrentConversation = await _chatService.GetActiveConversationAsync(buyerId.Value, sellerId.Value, ticketId.Value)
                                      ?? await _chatService.CreateConversationAsync(buyerId.Value, sellerId.Value, ticketId.Value);

                if (CurrentConversation != null)
                {
                    Messages = await _chatService.GetMessagesByConversationIdAsync(CurrentConversation.Id);
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostSendMessageAsync(Guid conversationId, string messageContent)
        {
            if (!string.IsNullOrWhiteSpace(messageContent) && UserId.HasValue)
            {
                var chat = new Chat
                {
                    Id = Guid.NewGuid(),
                    ConversationId = conversationId,
                    SellerId = UserId,
                    Message = messageContent,
                    SentAt = DateTime.UtcNow,
                    IsSender = true,
                };
                await _chatService.SendMessageAsync(chat);
            }
            return RedirectToPage(new { sellerId = conversationId });
        }

        public async Task<IActionResult> OnPostCreateTransactionAsync(Guid conversationId, Guid recipientId, Guid sellerId, Guid ticketId)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(ticketId);

            if (ticket == null)
            {
                return NotFound("Ticket not found for transaction creation.");
            }

            var approvalUrl = await _transactionService.CreateTransactionAsync(conversationId, recipientId, sellerId, (decimal)ticket.Price);

            if (!string.IsNullOrEmpty(approvalUrl))
            {
                var paymentMessage = new Chat
                {
                    Id = Guid.NewGuid(),
                    ConversationId = conversationId,
                    SellerId = UserId,
                    Message = $"Please complete the payment by clicking on this link: <a href='{approvalUrl}' target='_blank'>Pay Now</a>",
                    SentAt = DateTime.UtcNow,
                    IsSender = true,
                };

                await _chatService.SendMessageAsync(paymentMessage);

                TempData["Message"] = "Transaction created successfully! Payment link sent to the buyer.";
                return RedirectToPage("/Customer/Message", new { buyerId = recipientId, sellerId = sellerId });
            }

            TempData["Error"] = "Failed to create transaction.";
            return Page();
        }

        public async Task<IActionResult> OnGetSuccessAsync(string paymentId)
        {
            var config = new Dictionary<string, string>
            {
                { "mode", _configuration["PayPal:Mode"] },
                { "clientId", _configuration["PayPal:ClientId"] },
                { "clientSecret", _configuration["PayPal:ClientSecret"] }
            };

            var apiContext = new PayPal.Api.APIContext(new PayPal.Api.OAuthTokenCredential(config["clientId"], config["clientSecret"]).GetAccessToken())
            {
                Config = new Dictionary<string, string>
                {
                    { "mode", config["mode"] }
                }
            };

            var payment = Payment.Get(apiContext, paymentId);

            if (payment.state.ToLower() != "approved")
            {
                TempData["Error"] = "Payment was not approved.";
                return RedirectToPage("/Error");
            }

            var transaction = await _transactionService.GetByPayPalPaymentIdAsync(paymentId);
            if (transaction != null)
            {
                transaction.TransactionStatus = "Completed";
                await _transactionService.UpdateTransactionAsync(transaction);

                var ticket = await _ticketService.GetTicketByIdAsync(transaction.TicketId);
                if (ticket != null)
                {
                    ticket.TicketStatus = "Sold";
                    await _ticketService.UpdateTicketAsync(ticket);
                }

                await _transactionService.GetMoneyWalletFromBuyerAsync(transaction.SellerId, (decimal)transaction.Amount);

                TempData["Message"] = "Transaction completed successfully!";
                return RedirectToPage("/Customer/Profile", new { id = transaction.Id });
            }

            TempData["Error"] = "Transaction not found.";
            return RedirectToPage("/Error");
        }

        public IActionResult OnGetCancel()
        {
            TempData["Message"] = "Transaction was canceled.";
            return RedirectToPage("/Customer/Profile");
        }
    }
}