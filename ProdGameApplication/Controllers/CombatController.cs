using GameLibrary;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProdGameApplication.Contexts;
using ProdGameApplication.Hubs;
using ProdGameApplication.Interfaces;
using ProdGameApplication.Models.Game;
using System.Timers;

namespace ProdGameApplication.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]/[action]")]
    public class CombatController : ControllerBase
    {
        //private readonly ILogger<CombatController> _logger;
        private readonly IBalanceable _combatQuery;
        //private readonly GameContext _context;
        private readonly IHubContext<ChatHub> _hubContext;

        public CombatController(ILogger<CombatController> logger, IBalanceable combatQuery, IHubContext<ChatHub> hubContext)
        {
            //_logger = logger;
            _combatQuery = combatQuery;
            _hubContext = hubContext;
        }

        [HttpPost(Name = "AddToCombatQuery")]
        public async Task<IActionResult> AddToCombatQuery([FromBody] Player player)
        {
            _combatQuery.AddToCombatQuery(player);
            var players = _combatQuery
                .GetTwoPlayers()
                .ToArray();
           
            if(players.Length > 1)
            {
                var timer = new System.Timers.Timer();
                timer.Elapsed += (sender, args) => OnTimedEvent(sender, players[0].ConnectionId);
                timer.Interval = 30000;

                var combat = new ServerCombat
                {
                    Id = Guid.NewGuid(),
                    FirstPlayer = players[0],
                    SecondPlayer = players[1],
                    Timer = timer
                };

                _combatQuery.AddCombat(combat);

                foreach (var playerItem in players)
                {
                    await _hubContext.Clients.Client(playerItem.ConnectionId).SendAsync("broadcastMessage", "System", "You added to combat.");
                }

                timer.Start();
            }

            return Ok();
        }

        [HttpPost(Name = "MakeMove")]
        public async Task<IActionResult> MakeMove([FromBody] Move move)
        {
            return Ok();
        }

        private async void OnTimedEvent(object sender, string connectionId)
        {
            await _hubContext.Clients.Client(connectionId).SendAsync("broadcastMessage", "System", "Timer just worked.");
        }
    }
}
