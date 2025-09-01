using System;

namespace ChatBackend.Models
{
    public class GroupMessage
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int GroupId { get; set; }

        public string GroupName { get; set; } = string.Empty;
        public string MessageText { get; set; } = string.Empty;
        public DateTime SentAtUtc { get; set; }
    }
}
