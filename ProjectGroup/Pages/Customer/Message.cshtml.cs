using Azure.Messaging;
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
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;

        public Guid? UserId { get; private set; }
        public Guid? RoleId { get; private set; }
        public List<Conversation> Conversations { get; set; } = new List<Conversation>();
        public Conversation? CurrentConversation { get; set; }
        public List<Chat> Messages { get; set; } = new List<Chat>();
        public Transactions? Transactions { get; set; }

        public MessageModel(IChatService chatService, IHttpContextAccessor httpContextAccessor, ITransactionService transactionService,
                            ITicketService ticketService, IConfiguration configuration, IUserService userService, INotificationService notificationService)
        {
            _chatService = chatService;
            string userIdString = httpContextAccessor.HttpContext.Session.GetString("UserId");
            string roleIdString = httpContextAccessor.HttpContext.Session.GetString("RoleId");

            UserId = string.IsNullOrEmpty(userIdString) ? (Guid?)null : Guid.Parse(userIdString);
            RoleId = string.IsNullOrEmpty(roleIdString) ? (Guid?)null : Guid.Parse(roleIdString);
            _transactionService = transactionService;
            _ticketService = ticketService;
            _configuration = configuration;
            _userService = userService;
            _notificationService = notificationService;
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
                CurrentConversation = await _chatService.GetActiveConversationAsync(buyerId.Value, sellerId.Value, ticketId.Value);

                if (CurrentConversation == null)
                {
                    CurrentConversation = await _chatService.CreateConversationAsync(buyerId.Value, sellerId.Value, ticketId.Value);

                    await _notificationService.AddNotificationAsync(UserId, "Your conversation has been proceed");
                }

                Messages = await _chatService.GetMessagesByConversationIdAsync(CurrentConversation.Id);

                if (Transactions != null)
                {
                    Transactions = await _transactionService.GetTransactionBySomeIdsAsync(buyerId.Value, sellerId.Value, ticketId.Value);
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostSendMessageAsync(Guid conversationId, Guid? buyerId, Guid? sellerId, string messageContent)
        {
            var currentUserId = UserId;

            Guid? BuyerId = null;
            Guid? SellerId = null;

            if (currentUserId == buyerId)
            {
                BuyerId = currentUserId;
            }
            else if (currentUserId == sellerId)
            {
                SellerId = currentUserId;
            }

            Console.WriteLine($"Current User: {UserId} : Buyer: {buyerId} : Seller: {sellerId}");

            bool isSenderBuyer = BuyerId.HasValue;

            if (!string.IsNullOrWhiteSpace(messageContent) && currentUserId.HasValue)
            {
                var chat = new Chat
                {
                    Id = Guid.NewGuid(),
                    ConversationId = conversationId,
                    BuyerId = BuyerId,
                    SellerId = SellerId,
                    Message = messageContent,
                    SentAt = DateTime.UtcNow,
                    IsSender = isSenderBuyer
                };

                await _chatService.SendMessageAsync(chat);

                await _notificationService.AddNotificationAsync(BuyerId ?? SellerId, messageContent);
            }
            return RedirectToPage(new { conversationId = conversationId });
        }

        public async Task<IActionResult> OnPostCreateTransactionAsync(Guid conversationId, Guid buyerId, Guid sellerId, Guid ticketId)
        {
            var currentUserId = UserId;

            Guid? BuyerId = null;
            Guid? SellerId = null;

            var ticket = await _ticketService.GetTicketByIdAsync(ticketId);

            if (ticket == null)
            {
                TempData["ErrorMessage"] = "Ticket not found for transaction creation.";
                return Page();
            }

            var isTransactionCompleted = await _transactionService.CheckIsTransactionCompletedAsync(conversationId);
            if (isTransactionCompleted)
            {
                TempData["ErrorMessage"] = "This converstation has already completed transaction. Cannot create another transaction!!!";
                return Page();
            }

            var approvalUrl = await _transactionService.CreateTransactionAsync(conversationId, buyerId, sellerId, ticketId, (decimal)ticket.Price);

            if (currentUserId == buyerId)
            {
                BuyerId = currentUserId;
            }
            else if (currentUserId == sellerId)
            {
                SellerId = currentUserId;
            }

            bool isSenderBuyer = BuyerId.HasValue;

            if (!string.IsNullOrEmpty(approvalUrl) && currentUserId.HasValue)
            {
                var user = await _userService.GetUserByIdAsync(UserId);

                if (user != null)
                {
                    int totalReputationAdjustment = 0;

                    int reputationPointsPendingPunishment = await _transactionService.CheckBookingPunishmentForPendingAsync(UserId);
                    if (reputationPointsPendingPunishment > 0)
                    {
                        if (reputationPointsPendingPunishment == 3)
                            totalReputationAdjustment -= 4;
                        else if (reputationPointsPendingPunishment > 3 && reputationPointsPendingPunishment <= 6)
                            totalReputationAdjustment -= 5;
                        else if (reputationPointsPendingPunishment == 1 || reputationPointsPendingPunishment == 2)
                            totalReputationAdjustment -= 2;
                        else
                            totalReputationAdjustment -= 6;

                    }

                    user.ReputationPoints += totalReputationAdjustment;
                    await _userService.UpdateUserAsync(user);
                }

                var paymentMessage = new Chat
                {
                    Id = Guid.NewGuid(),
                    ConversationId = conversationId,
                    BuyerId = BuyerId,
                    SellerId = SellerId,
                    Message = $"Please complete the payment by clicking on this link: <a href='{approvalUrl}' target='_blank'>Pay Now</a>",
                    SentAt = DateTime.UtcNow,
                    IsSender = isSenderBuyer,
                };

                await _chatService.SendMessageAsync(paymentMessage);

                await _notificationService.AddNotificationAsync(buyerId, "Transaction created successfully! Payment link sent to the buyer.");

                TempData["SuccessMessage"] = "Transaction created successfully! Payment link sent to the buyer.";
                return Redirect($"http://localhost:5157/Customer/Message?buyerId={buyerId}&sellerId={sellerId}&ticketId={ticketId}");
            }

            TempData["ErrorMessage"] = "Failed to create transaction.";
            return Page();
        }
    }
}