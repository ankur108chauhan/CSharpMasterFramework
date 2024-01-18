using OpenQA.Selenium;
using Web.Pages;

namespace MasterFrameworkBDD.StepDefinitions
{
    [Binding]
    public sealed class LoginStepDefinitions
    {
        private readonly LoginPage loginPage;
        private readonly ScenarioContext _scenarioContext;

        public LoginStepDefinitions(IWebDriver driver, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            loginPage = new LoginPage(driver);
        }

        [Given(@"user navigates to ""([^""]*)""")]
        public void GivenUserNavigatesTo(string url)
        {
            loginPage.NavigateToURL(url);
        }

        [When(@"user login with valid credentials")]
        public void WhenUserLoginWithValidCredentials()
        {
            loginPage.Login("rahulshettyacademy", "learning");
        }
    }
}
