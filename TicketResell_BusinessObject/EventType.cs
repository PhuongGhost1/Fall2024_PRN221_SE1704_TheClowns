using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketResell_BusinessObject
{
    public partial class EventType
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Event Name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Event Name must contain only letters and spaces.")]
        public string? Name { get; set; }

        // Mối quan hệ với Ticket
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
