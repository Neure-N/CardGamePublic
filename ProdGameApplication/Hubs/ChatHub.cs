using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ProdGameApplication.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.SendAsync("broadcastMessage", name, message);

        }

        public void StartTimer()
        {
            Thread.Sleep(20000);
            Clients.Caller.SendAsync("broadcastMessage", "Timer", "Time is up");
        }
    }
}
