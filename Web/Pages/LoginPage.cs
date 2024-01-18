using OpenQA.Selenium;
using Web.Pages.JCMS;

namespace Web.Pages
{
    internal class LoginPage : BaseMethods
    {
        public By Username = By.Id("username");
        public By Password = By.Id("password");
        public By TermsCheckbox = By.Id("terms");
        public By SignInBtn = By.Id("signInBtn");

        public LoginPage(IWebDriver driver) : base(driver) { }

        public void Login(string username, string password)
        {
            EnterData(Username, username);
            EnterData(Password, password);
            ClickElement(TermsCheckbox);
            ClickElement(SignInBtn);
        }
    }
}
