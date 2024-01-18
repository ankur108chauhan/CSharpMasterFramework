using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace Mobile.Pages
{
    internal class LoginPage : BasePage
    {
        public By CheckoutBtn_Android = MobileBy.AccessibilityId("selected_language_text");
        public By CheckoutBtn_IOS = MobileBy.AccessibilityId("selected_language_text");

        public By EnglishLanguageSelectBtn_Android = MobileBy.XPath("//*[@resource-id='lang_selector_select_language_button_en']");
        public By EnglishLanguageSelectBtn_IOS = MobileBy.XPath("//*[@resource-id='lang_selector_select_language_button_en']");

        public By LoginBtn_Android = MobileBy.XPath("//*[@resource-id='btn_login' or @resource-id='vw_arrow_button_register']");
        public By LoginBtn_IOS = MobileBy.XPath("//*[@resource-id='btn_login' or @resource-id='vw_arrow_button_register']");

        public By PhoneNumberTextbox_Android = MobileBy.AccessibilityId("txt_input_field_phone");
        public By PhoneNumberTextbox_IOS = MobileBy.AccessibilityId("txt_input_field_phone");

        public By PasswordTextbox_Android = MobileBy.AccessibilityId("txt_login_field_password");
        public By PasswordTextbox_IOS = MobileBy.AccessibilityId("txt_login_field_password");

        public By AcceptCookesBtn = By.Id("truste-consent-button");

        public LoginPage(AppiumDriver driver) : base(driver) { }

        public void SelectEnglishLanguage()
        {
            TapElement(CheckoutBtn_Android, CheckoutBtn_IOS);
            TapElement(EnglishLanguageSelectBtn_Android, EnglishLanguageSelectBtn_IOS);
            TapElement(LoginBtn_Android, LoginBtn_IOS);
        }

        public void EnterLoginDetails(string phonenumber, string password)
        {
            EnterData(PhoneNumberTextbox_Android, PhoneNumberTextbox_IOS, phonenumber);
            TapElement(LoginBtn_Android, LoginBtn_IOS);
            EnterData(PasswordTextbox_Android, PasswordTextbox_IOS, password);
            TapElement(LoginBtn_Android, LoginBtn_IOS);
        }

        public bool IsWelcomePageHeadingTextPresent(string text)
        {
            ClickElement(AcceptCookesBtn, AcceptCookesBtn);
            return IsElementVisible(WebText(text), WebText(text));
        }
    }
}
