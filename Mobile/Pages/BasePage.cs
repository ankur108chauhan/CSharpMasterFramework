using Mobile.Enums;
using Mobile.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace Mobile.Pages
{
    internal class BasePage : BaseMethods
    {
        public By ProgressBar_Android = MobileBy.ClassName("android.widget.ProgressBar");
        public By ProgressBar_IOS = MobileBy.ClassName("android.widget.ProgressBar");
        public By WelcomeTitle_Android = MobileBy.AccessibilityId("welcome_title");
        public By WelcomeTitle_IOS = MobileBy.AccessibilityId("welcome_title");

        public By YesBtn = MobileBy.XPath("//*[@text='Yes']");
        public By Text(string text) => MobileBy.XPath("//*[@text='" + text + "']");
        public By WebText(string text) => MobileBy.XPath("//*[text()='" + text + "']");
        public By ContentDesc(string text) => MobileBy.XPath("//*[@content-desc='" + text + "']");

        public BasePage(AppiumDriver driver) : base(driver) { }

        public bool IsTextPresent(string text) => IsElementVisible(Text(text), Text(text));
        public bool IsWelcomeTitlePresent() => IsElementVisible(WelcomeTitle_Android, WelcomeTitle_IOS);
        public void ClickElementWithText(string text) => ScrollToElementTap(Text(text), Text(text), TouchDirection.DOWN);
        public void ClickElementWithContentDesc(string text) => TapElement(ContentDesc(text), ContentDesc(text));
    }
}
