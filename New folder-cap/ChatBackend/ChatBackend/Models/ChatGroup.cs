namespace ChatBackend.Models
{
    public class ChatGroup
    {
        public int Id { get; set; }   // changed from Guid → int
        public string Name { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

       // public ICollection<GroupMember> Members { get; set; } = new List<GroupMember>();
        //public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
