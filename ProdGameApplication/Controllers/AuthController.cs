using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProdGameApplication.Contexts;
using ProdGameApplication.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProdGameApplication.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly GameContext _context;
        private const int Lifetime = 24;

        public AuthController(ILogger<AuthController> logger, GameContext context, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Get jwt token for registered user.
        /// </summary>
        /// <param name="data">Authentification data.</param>
        [HttpPost(Name = "GetToken")]
        public async Task<IActionResult> Authorize([FromBody] AuthData data)
        {
            //Get user by login
            var user = await _userManager.FindByEmailAsync(data.Login);

            if(user == null)
                return NotFound();

            var hashedPassword = _userManager.PasswordHasher.HashPassword(user, data.Password);
            var verification = _userManager.PasswordHasher.VerifyHashedPassword(user, hashedPassword, data.Password);
            var roles = await _userManager.GetRolesAsync(user);

            if(verification == PasswordVerificationResult.Success)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
                };

                var roleClaims = roles
                    .Select(n => new Claim(ClaimsIdentity.DefaultRoleClaimType, n))
                    .ToArray();

                claims.AddRange(roleClaims);

                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("AuthData").GetSection("Key").Value));

                var jwt = new JwtSecurityToken(
                    issuer: _configuration.GetSection("AuthData").GetSection("Issuer").Value,
                    audience: _configuration.GetSection("AuthData").GetSection("Audience").Value,
                    notBefore: DateTime.UtcNow,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromHours(Lifetime)),
                    signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256));

                var encodedJwt = new JwtSecurityTokenHandler()
                    .WriteToken(jwt);

                var response = new
                {
                    username = user.UserName,
                    access_token = encodedJwt,
                };

                return Ok(response);
            }

            var incorrectPasswordResponse = new
            {
                response = "Incorrect password."
            };

            return Ok(incorrectPasswordResponse);
        }

        /// <summary>
        /// Register new user in system.
        /// </summary>
        /// <param name="data">Registration data.</param>
        [HttpPost(Name = "Register")]
        public async Task<IActionResult> Register([FromBody] RegisterData data)
        {
            if (data == null)
                return BadRequest("Data is null.");

            if (string.IsNullOrEmpty(data.Login) || string.IsNullOrEmpty(data.Password) || string.IsNullOrEmpty(data.RepeatPassword) || string.IsNullOrEmpty(data.Email))
                return BadRequest("Data if not full.");

            if (data.Password != data.Password)
                return BadRequest("Passwords does not match.");

            var user = new IdentityUser
            {
                Email = data.Email,
                UserName = data.Login
            };

            try
            {
                var result = await _userManager.CreateAsync(user, data.Password);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Auth", new { userId = user.Id, code }, protocol: HttpContext.Request.Scheme);

                var emailService = new EmailService(_configuration);
                await emailService.SendEmailAsync(data.Email, "Confirm your account", $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

                Thread.Sleep(5000);

                return Ok("Перейдите по адресу, указанному в почте.");
            }
            catch(Exception ex)
            {
                return BadRequest($"Произошла ошибка во время создания пользователя. {ex.Message}");
            }
        }
        
        /// <summary>
        /// Confirm email of new user.
        /// </summary>
        /// <param name="data">Email confirmation data.</param>
        [HttpGet(Name = "ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(code))
                return BadRequest("Data is not full.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest("User was not found.");

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
                return BadRequest("Error while email confirmmation.");

            return Ok("Email confirmed.");
        }
    }
}
