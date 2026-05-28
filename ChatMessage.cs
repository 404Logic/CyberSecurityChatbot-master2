namespace CyberSecurityChatbot
{
    public class ChatMessage
    {
        public string Sender { get; set; } = "";
        public string Message { get; set; } = "";
        public bool IsUser { get; set; }
    }
}
