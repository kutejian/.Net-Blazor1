using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAuth20.Common.Models
{
    public static class CookieTypeNames
    {
        /// <summary>
        /// Facebook Login AccessToken
        /// </summary>
        public static string AuthenticationUserAccessToken => "AuthenticationUserAccessToken";
        /// <summary>
        /// Facebook Login AccessToken
        /// </summary>
        public static string GoogleAccessToken => "GoogleAccessToken";

        /// <summary>
        /// Google User Id
        /// </summary>
        public static string GoogleUserId => "GoogleUserId";

        /// <summary>
        /// Google Email
        /// </summary>
        public static string GoogleEmail => "GoogleEmail";

        /// <summary>
        /// Github AccessToken
        /// </summary>
        public static string GithubAccessToken => "GithubAccessToken";
    }
}