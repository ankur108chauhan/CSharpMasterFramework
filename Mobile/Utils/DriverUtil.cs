
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Service;

namespace Mobile.Utils
{
    public class DriverUtil
    {
        private static readonly string userName = "Ankur";
        private static readonly string accessKey = "6579ac8c-6690-4d42-ad74-4781923c1233";
        public static readonly bool saucelabs = bool.Parse(TestContext.Parameters["SauceLabs"].ToString());
        public static AppiumLocalService service = null!;
        public ThreadLocal<AppiumDriver> driver = new ThreadLocal<AppiumDriver>();

        public AppiumDriver Initialize()
        {
            string platform = TestContext.Parameters["Platform"].ToString();

            if (platform.ToLower().Equals("android"))
            {
                if (saucelabs)
                    return new AndroidDriver(new Uri("https://ondemand.us-west-1.saucelabs.com:443/wd/hub"), GetRemoteAppiumOptions(platform), TimeSpan.FromMinutes(3));
                else
                {
                    return new AndroidDriver(service.ServiceUrl, GetAppiumOptions(platform));
                }
            }
            else
            {
                if (saucelabs)
                    return new IOSDriver(new Uri("https://ondemand.us-west-1.saucelabs.com:443/wd/hub"), GetRemoteAppiumOptions(platform), TimeSpan.FromMinutes(3));
                else
                    return new IOSDriver(service.ServiceUrl, GetAppiumOptions(platform));
            }
        }

        public AppiumOptions GetRemoteAppiumOptions(string platform)
        { 
            AppiumOptions appiumOptions = new AppiumOptions();
            var sauceOptions = new Dictionary<string, object>
            {
                { "username", userName },
                { "accessKey", accessKey },
                {"appiumVersion", "2.0.0"}
        };
            appiumOptions.AddAdditionalAppiumOption("sauce:options", sauceOptions);
            appiumOptions.AddAdditionalAppiumOption("autoGrantPermissions", true);

            if (platform.ToLower() == "android")
            {
                appiumOptions.AutomationName = "UiAutomator2";
                appiumOptions.PlatformName = "android";
                appiumOptions.PlatformVersion = "10";
                //appiumOptions.DeviceName = "Google Pixel 8 Pro GoogleAPI Emulator";
                appiumOptions.DeviceName = "Samsung.*";
                appiumOptions.App = "storage:9d915bd1-2705-463b-a421-53433300ec13";
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
