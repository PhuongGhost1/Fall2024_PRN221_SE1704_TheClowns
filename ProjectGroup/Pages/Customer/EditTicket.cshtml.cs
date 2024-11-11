using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketResell_BusinessObject;
using TicketResell_Service;

namespace ProjectGroup.Pages.Customer
{
    public class EditTicketModel : PageModel
    {
        private readonly ITicketService _ticketService;
        private readonly IEventService _eventService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IHostEnvironment _hostEnvironment;

        public EditTicketModel(ITicketService ticketService, IEventService eventService, IHttpContextAccessor contextAccessor,
                        IHostEnvironment hostEnvironment)
        {
            _ticketService = ticketService;
            _eventService = eventService;
            _contextAccessor = contextAccessor;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        [BindProperty]
        public Guid? UserId { get; set; }

        [BindProperty]
        public IFormFile CoverPhoto { get; set; }

        public List<SelectListItem> EventTypeOptions { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userIdString = _contextAccessor.HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }

            UserId = Guid.Parse(userIdString);

            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null || ticket.OwnerId != UserId)
            {
                return NotFound();
            }

            Ticket = ticket;
            EventTypeOptions = (await _eventService.GetAllEventsAsync())
                .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
                .ToList();

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please correct the errors and try again.";
                return Page();
            }

            var existingTicket = await _ticketService.GetTicketByIdAsync(Ticket.Id);
            if (existingTicket == null || existingTicket.OwnerId != UserId)
            {
                return NotFound();
            }

            existingTicket.EventName = Ticket.EventName;
            existingTicket.EventDate = Ticket.EventDate;
            existingTicket.EventTypeId = Ticket.EventTypeId;
            existingTicket.Price = Ticket.Price;
            existingTicket.ExpirationDate = Ticket.ExpirationDate;
            existingTicket.Serial = Ticket.Serial;

            if (CoverPhoto != null)
            {
                string folder = "tickets/cover/";
                string coverImageUrl = await UploadImage(folder, CoverPhoto);

                if (existingTicket.Images.Count > 0)
                {
                    var existingImage = existingTicket.Images.FirstOrDefault();
                    if (existingImage != null)
                    {
                        string oldImagePath = Path.Combine(_hostEnvironment.ContentRootPath, existingImage.Url.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                        existingImage.Url = coverImageUrl;
                        existingImage.UpdateAt = DateTime.Now;
                    }
                }
                else
                {
                    existingTicket.Images.Add(new Image
                    {
                        Id = Guid.NewGuid(),
                        TicketId = existingTicket.Id,
                        Url = coverImageUrl,
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now
                    });
                }
            }

            try
            {
                await _ticketService.UpdateTicketAsync(existingTicket);
                TempData["SuccessMessage"] = "Ticket updated successfully!";
            }
            catch (DbUpdateConcurrencyException)
            {
                TempData["ErrorMessage"] = "Error updating ticket. Please try again.";
                return NotFound();
            }

            return RedirectToPage("/Customer/EditTicket", new { id = existingTicket.Id });
        }
    }
}
