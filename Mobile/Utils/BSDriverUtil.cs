
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Service;

namespace Mobile.Utils
{
    public class BSDriverUtil
    {
        private static readonly string userName = "ankur_oD71Uj";
        private static readonly string accessKey = "C75zfy65hvQBb6jbUyBa";
        public static readonly bool browserstack = bool.Parse(TestContext.Parameters["Browserstack"].ToString());
        public static AppiumLocalService service = null!;
        public ThreadLocal<AppiumDriver> driver = new ThreadLocal<AppiumDriver>();

        public AppiumDriver Initialize()
        {
            string platform = TestContext.Parameters["Platform"].ToString();

            if (platform.ToLower().Equals("android"))
            {
                if (browserstack)
                    return new AndroidDriver(new Uri("https://hub.browserstack.com/wd/hub/"), GetBrowserstackAppiumOptions(platform), TimeSpan.FromMinutes(3));
                else
                {
                    return new AndroidDriver(service.ServiceUrl, GetAppiumOptions(platform));
                }
            }
            else
            {
                if (browserstack)
                    return new IOSDriver(new Uri("https://hub.browserstack.com/wd/hub/"), GetBrowserstackAppiumOptions(platform), TimeSpan.FromMinutes(3));
                else
                    return new IOSDriver(service.ServiceUrl, GetAppiumOptions(platform));
            }
        }

        public AppiumOptions GetBrowserstackAppiumOptions(string platform)
        {
            AppiumOptions appiumOptions = new AppiumOptions();
            Dictionary<string, object> browserstackOptions = new Dictionary<string, object>();
            browserstackOptions.Add("appiumVersion", "2.0.1");
            browserstackOptions.Add("userName", userName);
            browserstackOptions.Add("accessKey", accessKey);
            browserstackOptions.Add("interactiveDebugging", "true");
            browserstackOptions.Add("deviceLogs", "true");
            browserstackOptions.Add("networkLogs", "true");
            browserstackOptions.Add("appiumLogs", "true");

            appiumOptions.AddAdditionalAppiumOption("bstack:options", browserstackOptions);
            appiumOptions.AddAdditionalAppiumOption("autoGrantPermissions", true);

            if (platform.ToLower() == "android")
            {
                appiumOptions.AutomationName = "UiAutomator2";
                appiumOptions.PlatformName = "android";
                appiumOptions.PlatformVersion = "14.0";
                appiumOptions.DeviceName = "Google Pixel 8 Pro";
                appiumOptions.App = "bs://f9fde05672dff0e93df0f66a20c1997e781a4266";
            }
            else
            {
                appiumOptions.AutomationName = "XCUITest";
                appiumOptions.PlatformName = "ios";
                appiumOptions.PlatformVersion = "16";
                appiumOptions.DeviceName = "iPhone 14 Pro";
                appiumOptions.App = "";
            }
            return appiumOptions;
        }

        public AppiumOptions GetAppiumOptions(string platform)
        {
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalAppiumOption("autoGrantPermissions", true);

            if (platform.ToLower() == "android")
            {
                appiumOptions.AutomationName = "UiAutomator2";
                appiumOptions.PlatformName = "android";
                appiumOptions.DeviceName = "Pixel 7 Pro";
                string appPath = JsonHelper.GetProjectRootDirectory() + "/Resources/app.apk";
                appiumOptions.App = appPath;
            }
            else
            {
                appiumOptions.AutomationName = "XCUITest";
                appiumOptions.PlatformName = "ios";
                appiumOptions.DeviceName = "iPhone 17 Pro";
            }
            return appiumOptions;
        }
    }
}
