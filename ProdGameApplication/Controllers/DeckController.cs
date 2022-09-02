using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProdGameApplication.Contexts;
using ProdGameApplication.Models.Game;

namespace ProdGameApplication.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]/[action]")]
    public class DeckController : ControllerBase
    {
        private readonly GameContext _context;
        private readonly ILogger<DeckController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public DeckController(GameContext context, ILogger<DeckController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost(Name = "UpdateDeck")]
        public async Task<IActionResult> UpdateDeck([FromBody] UiDeck deck)
        {
            var userName = User.Identity?.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var cardsToDeck = _context.CardsToDecks.AsQueryable();
            var decks = _context.Decks.AsQueryable();
            var cards = _context.Cards.AsQueryable();
            var users = _context.Users.AsQueryable();

            var deckResult = from userItem in users
                             from card in cards
                             from ctd in cardsToDeck
                             from deckItem in decks 
                             where deckItem.UserId == user.Id
                             where ctd.DeckId == deckItem.Id
                       select ctd;

            var data = deckResult.ToArray();

            return Ok();
        }

        [HttpGet(Name = "GetCards")]
        public async Task<IActionResult> GetCards()
        {
            var username = User.Identity?.Name;

            var usersQuery = _context.Users.AsQueryable();
            var cardsToUsersQuery = _context.CardsToUsers.AsQueryable();
            var cardsQuery = _context.Cards.AsQueryable();

            var cards = from user in usersQuery
                        from cardId in cardsToUsersQuery
                        from card in cardsQuery
                        where user.UserName == username 
                        where card.Id == cardId.CardId
                        select card;

            var result = cards.ToArray();

            return Ok(result);
        }

        [HttpGet(Name = "GetDecks")]
        public async Task<IActionResult> GetDecks()
        {
            var username = User.Identity?.Name;
            var usersQuery = _context.Users.AsQueryable();
            var decksQuery = _context.Decks.AsQueryable();

            var decks = from deck in decksQuery
                        from user in usersQuery
                        where user.UserName == username
                        where deck.UserId == user.Id
                        select deck;

            var result = decks.ToArray();

            return Ok(result);
        }

        [HttpPost(Name = "GetDeck")]
        public async Task<IActionResult> GetDeck([FromBody] int deckId)
        {
            var decksQuery = _context.Decks.AsQueryable();
            var cardsToDecksQuery = _context.CardsToDecks.AsQueryable();
            var cardsQuery = _context.Cards.AsQueryable();

            var cards = from card in cardsQuery
                         from cardToDeck in cardsToDecksQuery
                         where cardToDeck.DeckId == deckId
                         group card by card.Name into cardSubQuery
                         select new
                         {
                             Name = cardSubQuery.Key,
                             Count = cardSubQuery.Count(),
                         };

            var result = cards.ToArray();

            return Ok();
        }
    }
}
