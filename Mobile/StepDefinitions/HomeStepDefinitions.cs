using Mobile.Enums;
using Mobile.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Appium;

namespace Mobile.StepDefinitions
{
    [Binding]
    public sealed class HomeStepDefinitions
    {
        private readonly HomePage homePage;
        private readonly ScenarioContext _scenarioContext;

        public HomeStepDefinitions(AppiumDriver driver, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            homePage = new HomePage(driver);
        }

        [Then(@"User should successfully login and logout")]
        public void ThenUsershouldsuccessfullyloginandlogout()
        {
            homePage.Logout("Log out");
            Assert.That(homePage.IsWelcomeTitlePresent(), Is.True);
        }

        [Then(@"User is able to swipe cards")]
        public void ThenUserisabletoswipecards()
        {
            homePage.SwipeToClickableBannerCard();
        }
    }
}
