using System;

namespace GameLibrary
{
    /// <summary>
    /// Class for containing data 
    /// </summary>
    public static class DataHolder
    {
        /// <summary>
        /// User's email.
        /// </summary>
        public static string Email { get; set; }
        /// <summary>
        /// Loged user's jwt token.
        /// </summary>
        public static string Token { get; set; }
        /// <summary>
        /// Token lifeteime begin.
        /// </summary>
        public static DateTime TokenGetTime { get; set; }
    }
}
