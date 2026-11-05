using OpenQA.Selenium;

namespace BankingProject.Tests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        
        private readonly By bankManagerLoginButton = By.CssSelector("button[ng-click='manager()']");

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        
        public void NavigateTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public ManagerPage LoginAsBankManager()
        {
            _driver.FindElement(bankManagerLoginButton).Click();
            return new ManagerPage(_driver);
        }
    }
}