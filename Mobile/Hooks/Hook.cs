using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using BoDi;
using Mobile.Utils;
using Mobile.Utils.ReportUtil;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;

namespace Mobile.Hooks
{
    [Binding]
    public class Hook : DriverUtil
    {
        private readonly IObjectContainer _objectContainer;
        private ScenarioContext _scenarioContext = null!;

        public Hook(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void GlobalSetup()
        {
            if (!saucelabs)
            {
                service = new AppiumServiceBuilder().UsingAnyFreePort().Build();
                service.Start();
            }
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            if (!saucelabs)
                service.Dispose();

            ExtentService.GetExtent().Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            ExtentTestManager.CreateFeature(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario(TestContext context)
        {
            driver.Value = Initialize();
            _objectContainer.RegisterInstanceAs<AppiumDriver>(driver.Value);
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
                var mediaEntity = CaptureScreenshot(_scenarioContext.ScenarioInfo.Title.Trim());

                //ExtentTestManager.GetStep().Fail(stepText);
                ExtentTestManager.GetStep().Fail(errorMessage);
                ExtentTestManager.GetStep().Fail(stackTrace);
                ExtentTestManager.GetStep().Fail("", mediaEntity);
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
            driver.Value.Quit();
        }

        private Media CaptureScreenshot(string name)
        {
            var screenshot = ((ITakesScreenshot)driver.Value).GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, name).Build();
        }
    }
}