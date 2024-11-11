using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketResell_BusinessObject;
using TicketResell_Service;

namespace ProjectGroup.Pages.Customer
{
    public class PaymentCancelModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ITransactionService _transactionService;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        public Guid? UserId { get; private set; }
        public Guid? RoleId { get; private set; }
        public PaymentCancelModel(IConfiguration configuration, ITransactionService transactionService, IHttpContextAccessor httpContextAccessor,
                                IChatService chatService, IUserService userService, INotificationService notificationService)
        {
            _configuration = configuration;
            _transactionService = transactionService;
            _chatService = chatService;
            _userService = userService;

            string userIdString = httpContextAccessor.HttpContext.Session.GetString("UserId");
            string roleIdString = httpContextAccessor.HttpContext.Session.GetString("RoleId");

            UserId = string.IsNullOrEmpty(userIdString) ? (Guid?)null : Guid.Parse(userIdString);
            RoleId = string.IsNullOrEmpty(roleIdString) ? (Guid?)null : Guid.Parse(roleIdString);
            _notificationService = notificationService;
        }
        public async Task<IActionResult> OnGetAsync(Guid transactionId)
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

            var transaction = await _transactionService.GetTransactionByIdAsync(transactionId);
            if (transaction != null)
            {
                transaction.TransactionStatus = "Cancel";
                await _transactionService.UpdateTransactionAsync(transaction);

                var user = await _userService.GetUserByIdAsync(UserId);

                if (user != null)
                {
                    int totalReputationAdjustment = 0;

                    int reputationPointsCancel = await _transactionService.CheckBookingPunishmentAsync(UserId, "Cancel");
                    if (reputationPointsCancel > 0)
                    {
                        if (reputationPointsCancel == 3)
                            totalReputationAdjustment -= 1;
                        else if (reputationPointsCancel > 3 && reputationPointsCancel < 7)
                            totalReputationAdjustment -= 2;
                        else if (reputationPointsCancel == 1 || reputationPointsCancel == 2)
                            totalReputationAdjustment -= 1;
                        else
                            totalReputationAdjustment -= 3;
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
                        Message = "Transaction canceled successfully! Please give us your reason...",
                        SentAt = DateTime.UtcNow,
                        IsSender = true
                    };

                    await _chatService.SendMessageAsync(chat);

                    await _notificationService.AddNotificationAsync(UserId, "Transaction canceled successfully! Please give us your reason...");
                }

                TempData["SuccessMessage"] = "Transaction was canceled.";
                return Page();
            }

            TempData["Error"] = "Transaction not found or already canceled.";
            return Page();
        }
    }
}
