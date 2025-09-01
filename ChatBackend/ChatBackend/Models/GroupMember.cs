namespace ChatBackend.Models
{
    public class GroupMember
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string UserId { get; set; } = default!;
        public string Role { get; set; } = "member";
    }
}