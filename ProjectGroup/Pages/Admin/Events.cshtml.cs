using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TicketResell_Service;
using System.Text.RegularExpressions;

namespace ProjectGroup.Pages.Admin
{
    public class EventsModel : PageModel
    {
        private readonly IEventService _eventService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventsModel(IHttpContextAccessor httpContextAccessor, IEventService eventService)
        {
            _httpContextAccessor = httpContextAccessor;
            _eventService = eventService;
        }

        public List<EventType> Events { get; set; } = new List<EventType>();

        [BindProperty]
        public EventType Event { get; set; } = new EventType();

        public async Task<IActionResult> OnGetAsync()
        {
            // Session validation logic
            string username = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            string roleIdString = _httpContextAccessor.HttpContext.Session.GetString("RoleId");
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(roleIdString) || !Guid.TryParse(roleIdString, out Guid roleId))
            {
                return RedirectToPage("/Authentication/Login");
            }

            // Role validation
            if (roleId != Guid.Parse("D569ADDF-590D-4D0F-B17A-F153CDB5BA6A"))
            {
                return RedirectToPage("/TicketsPage/Index");
            }

            // Fetch events
            Events = await _eventService.GetAllEventsAsync();
            return Page();
        }

        public async Task<IActionResult> OnGetUpdateAsync(Guid id)
        {
            var eventToUpdate = await _eventService.GetEventByIdAsync(id);

            if (eventToUpdate == null)
            {
                return NotFound();
            }

            Event = eventToUpdate;  // Bind the event details to the Event property
            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                // Refetch the event list to ensure it is displayed on the page
                Events = await _eventService.GetAllEventsAsync();
                return Page();
            }

            // Validate event name (Only letters, no numbers or special characters)
            if (string.IsNullOrEmpty(Event.Name) || !Regex.IsMatch(Event.Name, @"^[a-zA-Z\s]+$"))
            {
                TempData["Error"] = "Event name cannot be empty and must only contain letters and spaces.";

                // Refetch the event list to ensure it is displayed on the page
                Events = await _eventService.GetAllEventsAsync();
                return Page();
            }

            // Check if an event with the same name already exists
            bool eventExists = await _eventService.IsEventNameExistsAsync(Event.Name);
            if (eventExists)
            {
                TempData["Error"] = "An event with this name already exists.";

                // Refetch the event list to ensure it is displayed on the page
                Events = await _eventService.GetAllEventsAsync();
                return Page();
            }

            // Proceed to add the event if no duplication is found
            var eventType = new EventType
            {
                Id = Guid.NewGuid(),
                Name = Event.Name
            };

            await _eventService.AddEventTypeAsync(eventType);
            TempData["Success"] = "Event created successfully!";

            // Refetch the event list after the new event is created
            Events = await _eventService.GetAllEventsAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage();
            }

            // Validate event name (Only letters, no numbers or special characters)
            if (string.IsNullOrEmpty(Event.Name) || !Regex.IsMatch(Event.Name, @"^[a-zA-Z\s]+$"))
            {
                TempData["Error"] = "Event name cannot be empty and must only contain letters and spaces.";
                return RedirectToPage();
            }

            // Check if another event with the same name exists, excluding the current event
            bool eventExists = await _eventService.IsEventNameExistsAsync(Event.Name);
            if (eventExists && Event.Id != Guid.Empty)
            {
                // Add model error if event name is taken
                TempData["Error"] = "An event with this name already exists.";
                return RedirectToPage();
            }

            // Proceed to update the event
            var eventToUpdate = await _eventService.GetEventByIdAsync(Event.Id);
            if (eventToUpdate == null)
            {
                return NotFound();
            }

            eventToUpdate.Name = Event.Name;
            await _eventService.UpdateEventTypeAsync(eventToUpdate);

            TempData["Success"] = "Event updated successfully!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _eventService.DeleteEventTypeAsync(id);
            return RedirectToPage();
        }
    }
}
