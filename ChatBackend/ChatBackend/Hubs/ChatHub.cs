using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace YourNamespace.Hubs
{
    [Authorize] // require authentication
    public class ChatHub : Hub
    {
        // ✅ Private message by DisplayName
        public async Task SendPrivate(string receiverDisplayName, string message)
        {
            var sender = Context.User?.Identity?.Name ?? "Unknown";

            // Send to specific user by DisplayName
            await Clients.User(receiverDisplayName)
                .SendAsync("ReceiveMessage", sender, message, DateTime.UtcNow);
        }

        // ✅ Typing indicator
        public async Task Typing(string toDisplayName)
        {
            var sender = Context.User?.Identity?.Name ?? "Unknown";
            await Clients.User(toDisplayName)
                .SendAsync("Typing", sender);
        }

        // ✅ Group message
        public async Task SendGroup(string groupName, string message)
        {
            var sender = Context.User?.Identity?.Name ?? "Unknown";
            await Clients.Group(groupName)
                .SendAsync("ReceiveGroupMessage", sender, message, DateTime.UtcNow);
        }

        // ✅ Join a group
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        // ✅ Leave a group
        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
