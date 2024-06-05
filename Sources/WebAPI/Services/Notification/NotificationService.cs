using Microsoft.AspNetCore.SignalR;
using ModelLib.Model;

namespace WebAPI.Services.Notification
{
    public sealed class NotificationService : Hub
    {
        private static List<CurrentConnectionInfo> ConnectedUsers { get; set; } = new();
        
        public override Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            Console.WriteLine("connect method");

            if (httpContext is HttpContext context)
            {
                string userId = context.Request.Query["user"];

                Console.WriteLine($"User {userId} has joined");

                if(!ConnectedUsers.Any(cu => cu.UserId == userId))
                {
                    ConnectedUsers.Add(new()
                    {
                        ConnectedAt = DateTime.Now,
                        UserId = userId,
                        ConnectionId = Context.ConnectionId
                    });
                }
                else 
                {
                    var foundedUser = ConnectedUsers.FirstOrDefault(cu => cu.UserId == userId);
                    if(foundedUser != null) 
                    {
                        int index = ConnectedUsers.IndexOf(foundedUser);

                        ConnectedUsers[index].ConnectionId = Context.ConnectionId;
                    }
                }
            }
            else 
            {
                Console.WriteLine("Context is null");
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var removeUser = ConnectedUsers
                .Where(c => c.ConnectionId == Context.ConnectionId)
                .FirstOrDefault();

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
