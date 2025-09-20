namespace NotificationService.Domain
{
    public class MessageSendedDto
    {
        public int ChatId { get; set; }
        public int MessageId { get; set; }
        public DateTime SendTime { get; set; }
    }
}
