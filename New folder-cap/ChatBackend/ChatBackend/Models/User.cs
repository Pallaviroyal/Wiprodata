using Microsoft.AspNetCore.Identity;

namespace ChatBackend.Models
{
    public class User : IdentityUser
    {
        public string? DisplayName { get; set; }
        public string Status { get; set; } = "Available"; // Available | Busy |Offline
        public DateTime LastSeenUtc { get; set; } = DateTime.UtcNow;
    }
}
