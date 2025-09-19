namespace KafkaTest.Domain
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string User { get; set; } = "Anonymous";
        public string Text { get; set; } = string.Empty;
    }
}
