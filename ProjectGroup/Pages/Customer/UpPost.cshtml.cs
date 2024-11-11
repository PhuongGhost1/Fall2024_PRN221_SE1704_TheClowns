using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TicketResell_BusinessObject;
using TicketResell_Service;

namespace ProjectGroup.Pages.Customer 
{
    public class UpPostModel : PageModel
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ITicketService _ticketService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INotificationService _notificationService;

        [BindProperty]
        public Ticket Ticket { get; set; } = new Ticket();

        [BindProperty]
        public IFormFile CoverPhoto { get; set; }

        [BindProperty]
        public Guid? UserId { get; set; }

        public UpPostModel(IHostEnvironment hostEnvironment, ITicketService ticketService, IHttpContextAccessor httpContextAccessor,
                        INotificationService notificationService)
        {
            _hostEnvironment = hostEnvironment;
            _ticketService = ticketService;
            _httpContextAccessor = httpContextAccessor;
            _notificationService = notificationService;
        }

        public IActionResult OnGet()
        {
            string username = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            string roleIdString = _httpContextAccessor.HttpContext.Session.GetString("RoleId");
            string userIdString = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            Guid? roleId = string.IsNullOrEmpty(roleIdString) ? (Guid?)null : Guid.Parse(roleIdString);           
            UserId = string.IsNullOrEmpty(userIdString) ? (Guid?)null : Guid.Parse(userIdString);

            if (!string.IsNullOrEmpty(username) && roleId != null && UserId != null)
            {
                if (roleId != Guid.Parse("57FA54D1-7C67-4405-B277-B8A58049FF3C"))
                {
                    return RedirectToPage("/TicketsPage/Index");
                }
            }
            else
            {
                return RedirectToPage("/Authentication/Login");
            }

            return Page();
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", folderPath, uniqueFileName);
            Directory.CreateDirectory(Path.GetDirectoryName(serverFolder));
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderPath + uniqueFileName;
        }

        public async Task<IActionResult> OnPostAddTicketAsync()
        {
            if (ModelState.IsValid)
            {
                Ticket.OwnerId = UserId;

                if (CoverPhoto != null)
                {
                    string folder = "tickets/cover/";
                    string coverImageUrl = await UploadImage(folder, CoverPhoto);
                    Ticket.Images.Add(new Image { Id = Guid.NewGuid(), TicketId = Ticket.Id, Url = coverImageUrl, CreateAt = DateTime.Now, UpdateAt = DateTime.Now });
                }

                bool result = await _ticketService.AddTicketAsync(Ticket);
                if (result)
                {
                    await _notificationService.AddNotificationAsync(UserId, "Ticket added successfully! Waiting the buyer to looking for the post");

                    TempData["SuccessMessage"] = "Ticket added successfully!";
                    return RedirectToPage("/Customer/UpPost");
                }
            }
            TempData["ErrorMessage"] = "Error adding ticket.";
            return Page();
        }
    }
}
