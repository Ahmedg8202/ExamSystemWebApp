using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ExamSystem.API.Hubs
{
    public interface IHub
    {
        Task ReceiveExamNotification(string user, string message);
    }
    [Authorize(Roles = "Admin")]
    public class NotificationHub: Hub<IHub>
    {
        public override Task OnConnectedAsync()
        {
            if (Context.User.IsInRole("Admin"))
            {
                Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
            }

            return base.OnConnectedAsync();
        }
        public async Task sendMessageAsync(string user, string message)
        {
            await Clients.Group("Admins").ReceiveExamNotification(user, message);
        }

    }
}
