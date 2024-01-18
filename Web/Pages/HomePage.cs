using OpenQA.Selenium;
using Web.Enums;
using Web.Pages.JCMS;

namespace Web.Pages
{
    internal class HomePage : BaseMethods
    {
        public By CheckoutBtn = By.CssSelector("a[class='nav-link btn btn-primary']");

        public HomePage(IWebDriver driver) : base(driver) { }

        public bool IsHomePageElementPresent(HomePageEnums homePageEnum)
        {
            switch (homePageEnum)
            {
                case HomePageEnums.CHECKOUT_BTN:
                    return IsElementVisible(CheckoutBtn);
                default:
                    return false;
            }
        }
    }
}
