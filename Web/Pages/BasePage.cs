using OpenQA.Selenium;
using Web.Pages.JCMS;

namespace Web.Pages
{
    internal class BasePage : BaseMethods
    {
        public BasePage(IWebDriver driver) : base(driver) { }
    }
}
