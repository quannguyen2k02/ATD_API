namespace ATD_API.Hubs
{
    using Microsoft.AspNetCore.SignalR;

    public class NotificationHub : Hub
    {
        // Các phương thức từ client có thể gọi
        public async Task SendMessage(string user, string message)
        {
            // Gửi lại tin nhắn đến tất cả client
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        // Gửi thông báo đến client cụ thể
        public async Task SendPrivateMessage(string connectionId, string message)
        {
            await Clients.Client(connectionId).SendAsync("ReceivePrivateMessage", message);
        }

        // Gửi đến nhóm
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("GroupJoined", $"{Context.ConnectionId} joined group {groupName}");
        }
        public async Task JoinLedGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "LED_Group");
            Console.WriteLine($"Client {Context.ConnectionId} joined LED_Group");
        }
        public async Task JoinIoGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "IO_Group");
            Console.WriteLine($"Client {Context.ConnectionId} joined IO_Group");
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Client connected: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        // Xử lý khi client ngắt kết nối
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"Client disconnected: {Context.ConnectionId}");
            await base.OnDisconnectedAsync(exception);
        }
    }

}
