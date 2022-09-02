using GameLibrary;
using ProdGameApplication.Interfaces;
using ProdGameApplication.Models.Game;
using System.Collections.Concurrent;

namespace ProdGameApplication.Services
{
    public class CombatQueryService : IBalanceable
    {
        private readonly ConcurrentStack<Player> _combatQueryPlayers = new();
        private readonly ConcurrentDictionary<string, string> _connectedPlayers = new();
        private readonly ConcurrentBag<ServerCombat> _combats = new();

        /// <summary>
        /// Add a player to combat pool
        /// </summary>
        /// <param name="context"></param>
        /// <param name="player"></param>
        public void AddToCombatQuery(Player player)
        {
            _combatQueryPlayers.Push(player);
        }

        /// <summary>
        /// Try add connected player to common dictionary.
        /// </summary>
        /// <param name="username">Username from identity.</param>
        /// <param name="connectionId">SignalR connection id.</param>
        public bool TryAddToConnected(string username, string connectionId)
        {
            return _connectedPlayers.TryAdd(username, connectionId);
        }

        /// <summary>
        /// Try remove connected player from common dictionary.
        /// </summary>
        /// <param name="username">Username from identity.</param>
        /// <param name="connectionId">SignalR connection id.</param>
        public bool TryRemoveFromConnected(string username, string connectionId)
        {
            return _connectedPlayers.TryRemove(KeyValuePair.Create(username, connectionId));
        }

        /// <summary>
        /// Get to players from combat pool.
        /// </summary>
        /// <returns>Player</returns>
        public IEnumerable<Player> GetTwoPlayers()
        {
            Player firstPlayer;
            Player secondPlayer;

            if(_combatQueryPlayers.Count > 1)
            {
                var firstResult = _combatQueryPlayers.TryPop(out firstPlayer);
                var secondResult = _combatQueryPlayers.TryPop(out secondPlayer);

                if (firstResult && secondResult)
                    return new Player[] { firstPlayer, secondPlayer };
            }

            return Array.Empty<Player>();
        }

        /// <summary>
        /// Adds combat in combat collection.
        /// </summary>
        /// <param name="combat"></param>
        public void AddCombat(ServerCombat combat)
        {
            _combats.Add(combat);
        }

        /// <summary>
        /// Removes combat from combat collection.
        /// </summary>
        /// <param name="id">Combat id.</param>
        public void RemoveCombat(Guid id)
        {
            var combat = _combats.FirstOrDefault(n => n.Id == id);
            _combats.TryTake(out combat);
        }
    }
}
