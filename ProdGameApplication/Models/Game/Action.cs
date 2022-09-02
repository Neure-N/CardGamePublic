namespace ProdGameApplication.Models.Game
{
    public class Action
    {
        public Guid CombatId { get; set; }
        public string? ConnectionName { get; set; }
        public short Resources { get; set; }
        public List<UiDice>? DiceToResolve { get; set; }
        public List<UiDice>? DiceToRemove { get; set; }
        public List<UiDice>? DiceToRoll { get; set; }
        public List<UiDice>? DiceToTurn { get; set; }
        public List<UiCard>? CardsToPlay { get; set; }
        public List<UiCard>? CardToDiscard { get; set; } 
    }
}
