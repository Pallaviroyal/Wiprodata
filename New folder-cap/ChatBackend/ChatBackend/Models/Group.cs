namespace ChatBackend.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public ICollection<GroupMember> Members { get; set; } = new
        List<GroupMember>();
    }
}
