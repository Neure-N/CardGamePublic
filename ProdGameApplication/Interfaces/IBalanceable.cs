using GameLibrary;
using ProdGameApplication.Models.Game;

namespace ProdGameApplication.Interfaces
{
    public interface IBalanceable
    {
        void AddToCombatQuery(Player player);

        IEnumerable<Player> GetTwoPlayers();

        public bool TryAddToConnected(string username, string connectionId);

        public bool TryRemoveFromConnected(string username, string connectionId);

        public void AddCombat(ServerCombat combat);

        public void RemoveCombat(Guid id);
    }
}
