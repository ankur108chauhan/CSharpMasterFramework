using Mobile.Enums;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using RazorEngine.Compilation.ImpromptuInterface.Optimization;
using static OpenQA.Selenium.Interactions.PointerInputDevice;

namespace Mobile.Pages
{
    internal class BaseMethods
    {
        private const int TIMEOUT = 30;
        private const int SHORT_TIMEOUT = 2;
        private readonly AppiumDriver _driver;

        public BaseMethods(AppiumDriver driver)
        {
            _driver = driver;
        }

        public bool IsAndroid()
        {
            if (_driver.GetType().ToString().ToLower().Contains("android"))
                return true;
            else
                return false;
        }

        public By GetLocatorByPlatform(By androidBy, By iosBy)
        {
            if (IsAndroid())
                return androidBy;
            else
                return iosBy;
        }

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

        public void WaitForElementPresence(By androidBy, By iosBy, bool ignoreException = false, int timeout = TIMEOUT)
        {
            By locator = GetLocatorByPlatform(androidBy, iosBy);
            WaitForCondition(elem => elem.FindElements(locator).Count == 1, ignoreException, timeout);
        }

        public void WaitForElementAbsence(By androidBy, By iosBy, bool ignoreException = false, int timeout = TIMEOUT)
        {
            By locator = GetLocatorByPlatform(androidBy, iosBy);
            WaitForCondition(elem => elem.FindElements(locator).Count == 0, ignoreException, timeout);
        }

        public void WaitForElementVisibility(By androidBy, By iosBy, bool ignoreException = false, int timeout = TIMEOUT)
        {
            By locator = GetLocatorByPlatform(androidBy, iosBy);
            WaitForCondition(elem => elem.FindElement(locator).Displayed, ignoreException, timeout);
        }

        public void WaitForElementInVisibility(By androidBy, By iosBy, bool ignoreException = false, int timeout = TIMEOUT)
        {
            By locator = GetLocatorByPlatform(androidBy, iosBy);
            WaitForCondition(elem => elem.FindElements(locator).Count == 0, ignoreException, timeout);
        }

        public void WaitForContexts(int value, bool ignoreException = false, int timeout = TIMEOUT)
        {
            WaitForCondition(context => _driver.Contexts.Count == value, ignoreException, timeout);
        }

        public void ClickElement(By androidBy, By iosBy, bool ignoreException = false, int timeout = TIMEOUT)
        {
            By locator = GetLocatorByPlatform(androidBy, iosBy);
            WaitForElementVisibility(androidBy, iosBy, ignoreException, timeout);
            _driver.FindElement(locator).Click();
        }

        public void EnterData(By androidBy, By iosBy, string value, bool ignoreException = false, int timeout = TIMEOUT)
        {
            By locator = GetLocatorByPlatform(androidBy, iosBy);
            WaitForElementVisibility(androidBy, iosBy, ignoreException, timeout);
            _driver.FindElement(locator).SendKeys(value);
        }

        public bool IsElementVisible(By androidBy, By iosBy, bool ignoreException = false, int timeout = TIMEOUT)
        {
            By locator = GetLocatorByPlatform(androidBy, iosBy);
            WaitForElementVisibility(androidBy, iosBy, ignoreException, timeout);
            try
            {
                return _driver.FindElement(locator).Displayed;
            }
            catch
            {
                return false;
            }
        }

        public bool IsElementInVisible(By androidBy, By iosBy, bool ignoreException = false, int timeout = TIMEOUT)
        {
            By locator = GetLocatorByPlatform(androidBy, iosBy);
            WaitForElementVisibility(androidBy, iosBy, ignoreException, timeout);
            try
            {
                return _driver.FindElements(locator).Count == 0;
            }
            catch
            {
                return false;
            }
        }

        public void TapElement(By androidBy, By iosBy, bool ignoreException = false, int timeout = TIMEOUT)
        {
            By locator = GetLocatorByPlatform(androidBy, iosBy);
            WaitForElementVisibility(androidBy, iosBy, ignoreException, timeout);
            IWebElement element = _driver.FindElement(locator);

            PointerInputDevice touch = new PointerInputDevice(PointerKind.Touch, "finger");
            ActionSequence sequence = new ActionSequence(touch);

            PointerEventProperties pointerEventProperties = new PointerEventProperties
            {
                Pressure = 1
            };

            sequence.AddAction(touch.CreatePointerMove(element, 0, 0, TimeSpan.Zero));
            sequence.AddAction(touch.CreatePointerDown(MouseButton.Touch, pointerEventProperties));
            sequence.AddAction(touch.CreatePause(TimeSpan.FromSeconds(1)));
            sequence.AddAction(touch.CreatePointerUp(MouseButton.Touch, pointerEventProperties));

            List<ActionSequence> sequenceList = new() { sequence };
            _driver.PerformActions(sequenceList);
        }

        private void PerformScrollUsingSequence(int startX, int startY, int endX, int endY)
        {
            PointerInputDevice touch = new PointerInputDevice(PointerKind.Touch, "finger");
            ActionBuilder actionBuilder = new ActionBuilder();
            actionBuilder.AddAction(touch.CreatePointerMove(CoordinateOrigin.Viewport, startX, startY, TimeSpan.Zero));
            actionBuilder.AddAction(touch.CreatePointerDown(MouseButton.Touch));
            actionBuilder.AddAction(touch.CreatePointerMove(CoordinateOrigin.Viewport, endX, endY, TimeSpan.FromMilliseconds(300)));
            actionBuilder.AddAction(touch.CreatePointerUp(MouseButton.Touch));
            ((IActionExecutor)_driver).PerformActions(actionBuilder.ToActionSequenceList());
        }

        public void ScrollElementToCenter(By androidBy, By iosBy, bool ignoreException = false, int timeout = TIMEOUT)
        {
            By locator = GetLocatorByPlatform(androidBy, iosBy);
            WaitForElementVisibility(androidBy, iosBy, ignoreException, timeout);
            IWebElement element = _driver.FindElement(locator);

            int elementY = element.Location.Y;
            var size = _driver.Manage().Window.Size;

            int startX = size.Width / 2;
            int endX = size.Width / 2;
            int startY = elementY;
            int endY = size.Height / 2;

            PerformScrollUsingSequence(startX, startY, endX, endY);
        }

        public void ScrollElementToCenter(WebElement element)
        {
            int elementY = element.Location.Y;
            var size = _driver.Manage().Window.Size;

            int startX = size.Width / 2;
            int endX = size.Width / 2;
            int startY = elementY;
            int endY = size.Height / 2;

            PerformScrollUsingSequence(startX, startY, endX, endY);
        }

        public String GetCurrentPageSource()
        {
            return _driver.PageSource.ToString();
        }

        public bool IsNotEndOfPage(String previousPageSource)
        {
            return !previousPageSource.Equals(GetCurrentPageSource());
        }

        public void PerformScroll(TouchDirection direction)
        {
            var size = _driver.Manage().Window.Size;
            int startX = size.Width / 2;
            int endX = size.Width / 2;

            int startY;
            int endY;

            if (direction.Equals(TouchDirection.DOWN))
            {
                startY = (int)(size.Height * 0.50);
                endY = size.Height / 4;
            }
            else
            {
                startY = (int)(size.Height * 0.25);
                endY = size.Height;
            }
            PerformScrollUsingSequence(startX, startY, endX, endY);
        }

        public void PerformSwipe(TouchDirection direction)
        {
            var size = _driver.Manage().Window.Size;
            int startY = size.Height / 2;
            int endY = size.Height / 2;

            int startX;
            int endX;

            if (direction.Equals(TouchDirection.RIGHT))
            {
                startX = (int)(size.Width * 0.75);
                endX = 0;
            }
            else
            {
                startX = (int)(size.Width * 0.25);
                endX = (int)(size.Width * 1.00);
            }
            PerformScrollUsingSequence(startX, startY, endX, endY);
        }

        public void ScrollElementIntoView(By androidBy, By iosBy, TouchDirection direction)
        {
            String previousPageSource = "";
            while (IsElementInVisible(androidBy, iosBy, true, SHORT_TIMEOUT) && IsNotEndOfPage(previousPageSource))
            {
                previousPageSource = GetCurrentPageSource();
                PerformScroll(direction);
            }
        }

        public void SwipeElementIntoView(By androidBy, By iosBy, TouchDirection direction)
        {
            String previousPageSource = "";
            bool flag = false;
            while (IsElementInVisible(androidBy, iosBy, true) && !flag && IsNotEndOfPage(previousPageSource))
            {
                previousPageSource = GetCurrentPageSource();
                PerformSwipe(direction);
            }
        }

        public void ScrollToElementTap(By androidBy, By iosBy, TouchDirection direction)
        {
            ScrollElementIntoView(androidBy, iosBy, direction);
            TapElement(androidBy, iosBy);
        }

        public void SwitchToWebView()
        {
            WaitForContexts(2, true);
            if (IsAndroid())
                ((AndroidDriver)_driver).Context = "WEBVIEW_chrome";
            else
                ((IOSDriver)_driver).Context = "WEBVIEW_chrome";
        }

        public void SwitchToNativeApp()
        {
            if (IsAndroid())
            {
                int counter = 0;
                int maxCounter = 5;
                while (((AndroidDriver)_driver).CurrentActivity.Contains("chrome") && counter <= maxCounter)
                {
                    ((AndroidDriver)_driver).PressKeyCode(new KeyEvent().WithKeyCode(AndroidKeyCode.Back));
                    counter++;
                }
            }
            ((IContextAware)_driver).Context = "NATIVE_APP";
        }
    }
}
