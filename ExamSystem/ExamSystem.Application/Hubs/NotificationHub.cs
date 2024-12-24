using Microsoft.AspNetCore.SignalR;

namespace ExamSystem.API.Hubs
{
    public interface IHub
    {
        Task ReceiveExamNotification(string user, string message);
    }
    public class NotificationHub: Hub<IHub>
    {
        public void sendMessage(string user, string message)
        {
            Clients.All.ReceiveExamNotification(user, message);
        }
        //public async Task SendMessage()
        //    => await Clients.All.SendAsync("Student submit an exam");

        //public async Task SendMessage(string user, string message)
        //{ 
        //    await Clients.All.SendAsync("ReceiveMessage2", user, message); 
        //}

        //public async Task SendMessageToCaller(string user, string message)
        //    => await Clients.Caller.SendAsync("ReceiveMessage3", user, message);

        //public async Task SendMessageToGroup(string user, string message)
        //    => await Clients.Group("SignalR Users").SendAsync("ReceiveMessage4", user, message);

        //public async Task<string> WaitForMessage(string connectionId)
        //{
        //    var message = await Clients.Client(connectionId).InvokeAsync<string>(
        //        "GetMessage");
        //    return message;
        //}
    }
}
