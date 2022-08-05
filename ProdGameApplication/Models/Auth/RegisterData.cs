namespace ProdGameApplication.Models.Auth
{
    public class RegisterData : AuthData
    {
        public string? Email { get; set; }
        public string? RepeatPassword { get; set; }
    }
}
