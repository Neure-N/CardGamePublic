using Newtonsoft.Json;

namespace GameLibrary
{
    /// <summary>
    /// Class for registration data.
    /// </summary>
    public  class RegisterData : AuthData
    {
        /// <summary>
        /// Repeat password.
        /// </summary>
        [JsonProperty(PropertyName = "repeat_password")]
        public string RepeatPassword { get; set; }
    }
}
