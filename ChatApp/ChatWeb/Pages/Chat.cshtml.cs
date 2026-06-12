using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sales;

namespace MyApp.Namespace
{
    public class ChatModel : PageModel
    {
        private readonly UserService.UserServiceClient _userClient;
        private readonly ChatService.ChatServiceClient _chatClient;

        public List<UserMsg> Users { get; set; } = new();
        public List<MessageMsg> Messages { get; set; } = new();

        public string SelectedUserId { get; set; } = "";
        public string ChatRoomId { get; set; } = "";

        // dynamic logged-in user
        public string CurrentUserId { get; set; } = "1";

        public ChatModel(
            UserService.UserServiceClient userClient,
            ChatService.ChatServiceClient chatClient)
        {
            _userClient = userClient;
            _chatClient = chatClient;
        }

        // ---------------------------
        // LOAD PAGE + LOAD MESSAGES
        // ---------------------------
        public async Task OnGet(string? userId)
        {
            // detect login user from URL
            CurrentUserId = Request.Query["login"];
            if (string.IsNullOrEmpty(CurrentUserId))
                CurrentUserId = "1";

            // load all users
            var allUsers = await _userClient.GetAllUsersAsync(new Google.Protobuf.WellKnownTypes.Empty());
            Users = allUsers.Users.ToList();

            if (string.IsNullOrEmpty(userId))
                return;

            SelectedUserId = userId;

            // get chatroom
            var room = await _chatClient.CreateChatRoomAsync(
                new ChatRoomRequest
                {
                    User1Id = CurrentUserId,
                    User2Id = userId
                });

            ChatRoomId = room.ChatRoom.ChatRoomId;

            // load messages
            var msgResponse = await _chatClient.GetMessagesAsync(
                new GetMessagesRequest { ChatRoomId = ChatRoomId });

            Messages = msgResponse.Messages.ToList();
        }

        // ---------------------------
        // SEND MESSAGE
        // ---------------------------
        public async Task<IActionResult> OnPostSend(string login, string userId, string chatRoomId, string messageText)
        {
            // fetch logged-in user
            CurrentUserId = login;
            if (string.IsNullOrEmpty(CurrentUserId))
                CurrentUserId = "1";

            if (string.IsNullOrWhiteSpace(messageText))
                return RedirectToPage(new { login = login, userId = userId });

            await _chatClient.SendMessageAsync(new SendMessageRequest
            {
                ChatRoomId = chatRoomId,
                SenderId = CurrentUserId,
                MessageText = messageText
            });

            return RedirectToPage(new { login = login, userId = userId });
        }

        // ---------------------------
        // AUTO REFRESH
        // ---------------------------
        public async Task<IActionResult> OnGetRefresh(string login, string userId)
        {
            // Fix null login bug
            CurrentUserId = login;
            if (string.IsNullOrEmpty(CurrentUserId))
                CurrentUserId = Request.Query["login"];
            if (string.IsNullOrEmpty(CurrentUserId))
                CurrentUserId = "1";

            var room = await _chatClient.CreateChatRoomAsync(
                new ChatRoomRequest
                {
                    User1Id = CurrentUserId,
                    User2Id = userId
                });

            var messages = await _chatClient.GetMessagesAsync(
                new GetMessagesRequest
                {
                    ChatRoomId = room.ChatRoom.ChatRoomId
                });

            return Partial("_MessagesPartial", messages.Messages);
        }

    }
}
