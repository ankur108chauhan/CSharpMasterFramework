
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Web.Pages.JCMS
{
    internal class BaseMethods
    {
        private const int TIMEOUT = 30;
        private const int SHORT_TIMEOUT = 2;
        private readonly IWebDriver _driver;

        public BaseMethods(IWebDriver driver)
        {
            _driver = driver;
        }

        public void NavigateToURL(string URL) => _driver.Navigate().GoToUrl(URL);

        private void WaitForCondition(Func<IWebDriver, bool> condition, bool ignoreException = false, int timeout = TIMEOUT)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeout));
                wait.Until(condition);
            }
            catch (Exception e)
            {
                if (!ignoreException)
                {
                    Assert.Fail(e.Message);
                }
            }
        }

        public void WaitForElementPresence(By locator, bool ignoreException, int timeout)
        {
            WaitForCondition(elem => elem.FindElements(locator).Count == 1, ignoreException, timeout);
        }

        public void WaitForElementAbsence(By locator, bool ignoreException, int timeout)
        {
            WaitForCondition(elem => elem.FindElements(locator).Count == 0, ignoreException, timeout);
        }

        public void WaitForElementVisibility(By locator, bool ignoreException, int timeout)
        {
            WaitForCondition(elem => elem.FindElement(locator).Displayed, ignoreException, timeout);
        }

        public void ClickElement(By locator, bool ignoreException = false, int timeout = TIMEOUT)
        {
            WaitForElementVisibility(locator, ignoreException, timeout);
            _driver.FindElement(locator).Click();
        }

        public void EnterData(By locator, string value, bool ignoreException = false, int timeout = TIMEOUT)
        {
            WaitForElementVisibility(locator, ignoreException, timeout);
            _driver.FindElement(locator).SendKeys(value);
        }

        public bool IsElementVisible(By locator, bool ignoreException = false, int timeout = TIMEOUT)
        {
            WaitForElementVisibility(locator, ignoreException, timeout);
            try
            {
                return _driver.FindElement(locator).Displayed;
            }
            catch
            {
                return false;
            }
        }
    }
}
