using Mobile.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace Mobile.Pages
{
    internal class HomePage : BasePage
    {
        public By ProfileIcon_Android = MobileBy.XPath("//*[@resource-id='icon_header_right']");
        public By ProfileIcon_IOS = MobileBy.XPath("//*[@resource-id='icon_header_right']");
        public By ClickableBannerCard_Android = MobileBy.XPath("//*[@resource-id='carousel-announcements-text-nectarine']");
        public By ClickableBannerCard_IOS = MobileBy.XPath("//*[@resource-id='carousel-announcements-text-nectarine']");
        public By CardsCarousel_Android = MobileBy.XPath("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[1]");
        public By CardsCarousel_IOS = MobileBy.XPath("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[1]");

        public HomePage(AppiumDriver driver) : base(driver) { }

        public void Logout(string buttonText)
        {
            TapElement(ProfileIcon_Android, ProfileIcon_IOS);
            ScrollToElementTap(Text(buttonText), Text(buttonText), TouchDirection.DOWN);
            TapElement(YesBtn, YesBtn);
        }

        public void SwipeToClickableBannerCard()
        {
            WaitForElementVisibility(ProgressBar_Android, ProgressBar_IOS);
            WaitForElementInVisibility(ProgressBar_Android, ProgressBar_IOS);
            //ScrollElementIntoView(CardsCarousel_Android, CardsCarousel_IOS, TouchDirection.DOWN);
            PerformScroll(TouchDirection.DOWN);
            SwipeElementIntoView(ClickableBannerCard_Android, ClickableBannerCard_IOS, TouchDirection.RIGHT);
        }
    }
}
