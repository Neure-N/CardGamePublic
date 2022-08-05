using Newtonsoft.Json;

namespace GameLibrary.Authentification
{
    /// <summary>
    /// Class for parsing Json response.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Access token for authorized interactions.
        /// </summary>
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
    }
}
