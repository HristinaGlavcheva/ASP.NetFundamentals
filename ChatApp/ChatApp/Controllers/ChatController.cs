using ChatApp.Models.Chat;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {
        private static IList<KeyValuePair<string, string>> messages =
            new List<KeyValuePair<string, string>>();

        public IActionResult Show()
        {
            if (messages.Count < 1)
            {
                return View(new ChatViewModel());
            }

            var chatViewModel = new ChatViewModel()
            {
                Messages = messages.Select(m => new MessageViewModel
                {
                    Sender = m.Key,
                    MessageText = m.Value
                }).ToList()
            };

            return View(chatViewModel);
        }

        [HttpPost]
        public IActionResult Send(ChatViewModel chat)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Show));
            }

            var newMessage = chat.CurrentMessage;

            messages.Add(new KeyValuePair<string, string>(newMessage.Sender, newMessage.MessageText));
            
            return RedirectToAction(nameof(Show));
        }
    }
}
