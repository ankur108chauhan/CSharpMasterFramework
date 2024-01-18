using Mobile.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Appium;

namespace Mobile.StepDefinitions
{
    [Binding]
    public sealed class LoginStepDefinitions
    {
        private readonly LoginPage loginPage;
        private readonly ScenarioContext _scenarioContext;

        public LoginStepDefinitions(AppiumDriver driver, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            loginPage = new LoginPage(driver);
        }

        [Given(@"User select English language and click Login Button")]
        public void GivenUserSelectEnglishLanguageAndClickLoginButton()
        {
            loginPage.SelectEnglishLanguage();
        }

        [When(@"User enters phone number ""([^""]*)"" and password ""([^""]*)""")]
        public void WhenUserEntersPhoneNumberAndPassword(string phonenumber, string password)
        {
            loginPage.EnterLoginDetails(phonenumber, password);
        }

        [Then(@"Error message ""([^""]*)"" should be displayed")]
        public void ThenErrorMessageShouldBeDisplayed(string text)
        {
            Assert.That(loginPage.IsTextPresent(text), Is.True, text + " is not present");
        }


        [When(@"User clicks on ""(.*)"" button")]
        public void WhenUserclicksonbutton(string text)
        {
            loginPage.ClickElementWithText(text);
        }

        [When(@"User clicks on ""(.*)"" link")]
        public void WhenUserclicksonlink(string url)
        {
            loginPage.ClickElementWithContentDesc(url);
        }

        [Then(@"User is able to switch to webview and back to native app")]
        public void ThenUserisabletoswitchtowebviewandbacktonativeapp()
        {
            loginPage.SwitchToWebView();
            loginPage.IsWelcomePageHeadingTextPresent("Добро пожаловать");
            loginPage.SwitchToNativeApp();
        }
    }
}
