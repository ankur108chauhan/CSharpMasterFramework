
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Web.Utils
{
    public class DriverUtil
    {
        public IWebDriver driver = null!;

        public IWebDriver Initialize()
        {
            try
            {
                driver = SetDriver("chrome");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            return driver;
        }

        private IWebDriver SetDriver(string browser)
        {
            driver = browser.ToLower() switch
            {
                "chrome" => InitChromeDriver(),
                "firefox" => InitChromeDriver(),
                _ => InitChromeDriver(),
            };
            return driver;
        }

        private IWebDriver InitChromeDriver()
        {
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArgument("--headless");
            //chromeOptions.AddArgument("--window-size=1920,1080");
            chromeOptions.AddArgument("--incognito");
            chromeOptions.AddArgument("--start-maximized");
            driver = new ChromeDriver(chromeOptions);
            return driver;
        }
    }
}
