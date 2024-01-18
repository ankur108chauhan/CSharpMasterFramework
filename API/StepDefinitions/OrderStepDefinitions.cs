using System.Net;
using API.Endpoints;
using API.Methods;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Api.Tests.StepDefinitions
{
    [Binding]
    public sealed class ApiStepDefinitions
    {
        RestResponse restResponse = null!;
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;

        public ApiStepDefinitions(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
        }

        [When(@"I send GET API request for ""(.*)"" API")]
        public void WhenIsendGETAPIrequestforAPI(string apiName)
        {
            var headers = BaseMethods.GetCommonHeaders("1176577", "7", "9042798783@pepsiconnect.com");
            if (apiName == "Order")
                restResponse = BaseMethods.SendGETRequest(headers, Endpoints.ORDER_ENDPOINT);
        }


        [Then(@"I got ""(.*)"" status code")]
        public void ThenIgotstatuscode(string statusCode)
        {
            HttpStatusCode code = restResponse.StatusCode;
            int numericStatusCode = (int)code;
            System.Console.WriteLine(numericStatusCode);
            var content = restResponse.Content;
            dynamic json = JObject.Parse(content);
            JArray orders = json.orders;
            System.Console.WriteLine(orders[0]);
        }
    }
}