using Microsoft.AspNetCore.SignalR;
using ModelLib.Model;

namespace WebAPI.Services.Notification
{
    public class NotificationService : Hub
    {
        private List<CurrentConnectionInfo> ConnectedUsers { get; set; } = new();
        
        public override Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            if (httpContext is HttpContext context)
            {
                string userId = context.Request.Headers["user-id"].ToString();

                Console.WriteLine($"User {userId} has joined");

                ConnectedUsers.Add(new()
                {
                    ConnectedAt = DateTime.Now,
                    UserId = userId,
                    ConnectionId = Context.ConnectionId
                });
            }
            else 
            {
                Console.WriteLine("Context is null");
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var removeUser = ConnectedUsers.Where(c => c.ConnectionId == Context.ConnectionId).FirstOrDefault();

            if (removeUser != null)
            {
                ConnectedUsers.Remove(removeUser);
                Console.WriteLine($"{Context.ConnectionId} has disconnected");
            }

            return base.OnDisconnectedAsync(exception);
        }

        
        public async Task SendNotification(string message)
        {
            //TODO: Сделать так что бы при обновлении заявки появлось уведомление
        }
    }
}
