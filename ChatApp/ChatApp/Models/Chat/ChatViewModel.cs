namespace ChatApp.Models.Chat
{
    public class ChatViewModel
    {
        public ChatViewModel()
        {
            Messages = new HashSet<MessageViewModel>();
        }

        public MessageViewModel CurrentMessage { get; set; } = null!;

        public ICollection<MessageViewModel> Messages { get; set; }   
    }
}
