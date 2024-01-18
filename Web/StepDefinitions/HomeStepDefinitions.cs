using OpenQA.Selenium;
using Web.Enums;
using Web.Pages;

namespace MasterFrameworkBDD.StepDefinitions
{
    [Binding]
    public sealed class HomeStepDefinitions
    {
        private readonly HomePage homePage;
        private readonly ScenarioContext _scenarioContext;

        public HomeStepDefinitions(IWebDriver driver, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            homePage = new HomePage(driver);
        }

        [Then(@"user should be able to successfully login")]
        public void ThenUserShouldBeAbleToSuccessfullyLogin()
        {
            homePage.IsHomePageElementPresent(HomePageEnums.CHECKOUT_BTN).Should().BeTrue();
        }
    }
}
