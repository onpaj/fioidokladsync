using IdokladSdk;
using IdokladSdk.Clients.Auth;

namespace iDokladSync
{
    public class IDokladConnector
    {
        public static IdokladSdk.ApiExplorer Connect(string clientId, string clientSecret)
        {
            var credentials = new ClientCredentialAuth(clientId, clientSecret);

            var apiContext = new ApiContext(credentials)
            {
                AppName = "iDoklad",
            };

            var explorer = new IdokladSdk.ApiExplorer(apiContext);

            return explorer;
        }
    }
}