namespace NotificationService.Domain
{
    public class MessageSendedDto
    {
        public string ChatId { get; set; }
        public string MessageId { get; set; }
        public DateTime SendTime { get; set; }
    }
}
