namespace ChatApp.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Guid ChatRoomId { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
        public ChatRoom ChatRoom { get; set; }
    }
}
