using System;
using System.Collections.Generic;

namespace TicketResell_BusinessObject;

public partial class User
{
    public Guid Id { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Username { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ReputationPoints { get; set; }

    public string? Password { get; set; }

    public bool? IsVerified { get; set; }

    public decimal? Wallet { get; set; }

    public virtual ICollection<Chat> ChatBuyers { get; set; } = new List<Chat>();

    public virtual ICollection<Chat> ChatSellers { get; set; } = new List<Chat>();

    public virtual ICollection<Conversation> ConversationBuyers { get; set; } = new List<Conversation>();

    public virtual ICollection<Conversation> ConversationSellers { get; set; } = new List<Conversation>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<SocialMedium> SocialMedia { get; set; } = new List<SocialMedium>();

    public virtual ICollection<Support> Supports { get; set; } = new List<Support>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual ICollection<Transactions> TransactionBuyers { get; set; } = new List<Transactions>();

    public virtual ICollection<Transactions> TransactionSellers { get; set; } = new List<Transactions>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
