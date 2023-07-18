using PayPal.Api;
using System.Collections.Generic;

namespace MyWebSite.Models
{
    public class PaypalConfiguration
    {
        public static Dictionary<string, string> GetConfiguration(string mode)
        {
            return new Dictionary<string, string>()
        {
            {"mode",mode }
        };
        }

        private static string GetAccessToken(string ClientId, string ClientSecret, string mode)
        {
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, new Dictionary<string, string>(){{"mode",mode }}).GetAccessToken();
            return accessToken;
        }

        public static APIContext GetAPIContext(string clientId, string clientSecret, string mode)
        {
            APIContext apiContext = new APIContext(GetAccessToken(clientId, clientSecret, mode));
            apiContext.Config = GetConfiguration(mode); 
            return apiContext;
        }
    }


}
