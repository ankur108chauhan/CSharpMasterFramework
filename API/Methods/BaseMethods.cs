using System.Collections;
using RestSharp;

namespace API.Methods
{
    internal class BaseMethods
    {
        public static RestClient? restClient;

        public static ICollection<KeyValuePair<string, string>> GetCommonHeaders(string storeId, string siteId, string accessToken)
        {
            var header = new Dictionary<string, string>
            {
                { "storeId", storeId},
                { "siteId", siteId },
                { "okta-accesstoken", accessToken },
                { "Cache-Control", "no-cache" },
                { "Accept", "*/*" }
            };
            return header;
        }

        public static RestResponse SendGETRequest(ICollection<KeyValuePair<string, string>> headers, string endpoint)
        {
            RestRequest restRequest = new(endpoint);
            restRequest.AddHeaders(headers);
            return restClient.Get(restRequest);
        }
    }
}
