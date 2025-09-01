using System;

namespace ChatBackend.Models
{
    public class Message
    {
        public int Id { get; set; }

        // must match ApplicationUser.Id (which is string by default in IdentityUser)
        public string? SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public int? GroupId { get; set; }

        public string Content { get; set; } = string.Empty;

        // navigation props (optional, but good practice)
        public string? AttachmentUrl { get; set; }
        public DateTime SentAtUtc { get; set; } = DateTime.UtcNow;
    }
}
