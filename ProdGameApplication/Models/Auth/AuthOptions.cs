using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProdGameApplication.Models.Auth
{
    public class AuthOptions
    {
        public const string Issuer = "KenAuthServer";
        public const string Audience = "KenAuthClient";
        const string Key = "b8a300c7-e17a-47od-bf18-5d7af3275de4";
        public const int Lifetime = 24;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
