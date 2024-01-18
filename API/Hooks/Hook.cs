using API.Methods;
using API.Utils.ReportUtil;
using BoDi;
using NUnit.Framework;
using RestSharp;

namespace API.Hooks
{
    [Binding]
    internal class Hook : BaseMethods
    {
        private readonly IObjectContainer _objectContainer;
        private ScenarioContext _scenarioContext = null!;

        public Hook(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            ExtentService.GetExtent().Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            var options = new RestClientOptions("https://admin.cep.qa.ru.pepsico.com/");
            restClient = new RestClient(options);
            ExtentTestManager.CreateFeature(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario(TestContext context)
        {
            ExtentTestManager.CreateScenario(context.Test.Name);
        }

        [BeforeStep]
        public void BeforeStep()
        {
            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            var stepText = _scenarioContext.StepContext.StepInfo.Text;
            ExtentTestManager.CreateStep(stepType, stepText);
        }

        [AfterStep]
        public void InsertReportingSteps()
        {
            var stepText = _scenarioContext.StepContext.StepInfo.Text;
            if (_scenarioContext.TestError == null)
            {
                // ExtentTestManager.GetStep().Pass(stepText);
            }
            else if (_scenarioContext.TestError != null)
            {
                var errorMessage = string.IsNullOrEmpty(_scenarioContext.TestError.Message) ? "" : string.Format("<pre>{0}</pre>", _scenarioContext.TestError.Message);
                var stackTrace = string.IsNullOrEmpty(_scenarioContext.TestError.StackTrace) ? "" : string.Format("<pre>{0}</pre>", _scenarioContext.TestError.StackTrace);

                //ExtentTestManager.GetStep().Fail(stepText);
                ExtentTestManager.GetStep().Fail(errorMessage);
                ExtentTestManager.GetStep().Fail(stackTrace);
            }
            if (_scenarioContext.ScenarioExecutionStatus.ToString() == "StepDefinitionPending")
            {
                var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
                ExtentTestManager.CreateStep(stepType, stepText).Skip("Step Definition Pending");
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {

        }
    }
}