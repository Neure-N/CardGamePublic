using Newtonsoft.Json;

namespace GameLibrary
{
    /// <summary>
    /// Class for authentification data.
    /// </summary>
    public class AuthData
    {   
        /// <summary>
        /// Login for authorization.
        /// </summary>
        [JsonProperty(PropertyName = "login")]
        public string Login { get; set; }
        /// <summary>
        /// Password for authorization.
        /// </summary>
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}