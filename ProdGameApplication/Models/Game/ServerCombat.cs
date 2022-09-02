using GameLibrary;

namespace ProdGameApplication.Models.Game
{
    public class ServerCombat
    {
        public Guid Id { get; set; }
        public Player? FirstPlayer { get; set; }
        public Player? SecondPlayer { get; set; }
        public System.Timers.Timer? Timer { get; set; }
    }
}
