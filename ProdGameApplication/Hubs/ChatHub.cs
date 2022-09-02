using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using ProdGameApplication.Interfaces;

namespace ProdGameApplication.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub
    {
        private readonly IBalanceable _combatQuery;

        public ChatHub(IBalanceable combatQuery)
        {
            _combatQuery = combatQuery;
        }

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.SendAsync("broadcastMessage", name, message);
        }

        public override Task OnConnectedAsync()
        {
            var username = Context.User?.Identity?.Name;
            var connectionId = Context.ConnectionId;

            _combatQuery.TryAddToConnected(username, connectionId);

            Clients.Caller.SendAsync("getConnectionId", connectionId);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var username = Context.User?.Identity?.Name;
            var connectionId = Context.ConnectionId;

            _combatQuery.TryRemoveFromConnected(username, connectionId);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
