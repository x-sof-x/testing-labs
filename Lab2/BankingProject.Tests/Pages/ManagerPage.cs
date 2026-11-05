using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BankingProject.Tests.Pages
{
    public class ManagerPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        
        private readonly By customersTab = By.CssSelector("button[ng-click='showCust()']");

        
        private readonly By searchInput = By.CssSelector("input[ng-model='searchCustomer']");

        
        private readonly By tableRows = By.CssSelector("table tbody tr");

        public ManagerPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void OpenCustomersTab()
        {
            
            var tab = _wait.Until(d => d.FindElement(customersTab));
            tab.Click();

            
            _wait.Until(d => d.FindElement(searchInput));
        }

        public void SearchCustomer(string name)
        {
            var search = _wait.Until(d => d.FindElement(searchInput));
            search.Clear();
            search.SendKeys(name);
        }

        public bool DeleteCustomer(string firstName)
        {
            
            var row = _wait.Until(d =>
                d.FindElements(tableRows).FirstOrDefault(r => r.Text.Contains(firstName))
            );

            if (row == null)
                return false;

            var deleteBtn = row.FindElement(By.CssSelector("button[ng-click*='deleteCust']"));
            deleteBtn.Click();
            return true;
        }

        public bool IsCustomerPresent(string firstName)
        {
            
            return _driver.FindElements(tableRows).Any(r => r.Text.Contains(firstName));
        }
    }
}