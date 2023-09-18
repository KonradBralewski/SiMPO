namespace Core.Infrastracture.Persistence.Entities
{
    public class ChatMessage
    {
        public Guid Id { get; set; }   
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; } = null!;

        public virtual ApplicationUser User { get; set; } = null!;
    }
}
