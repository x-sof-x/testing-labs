using BankingProject.Tests.Pages;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BankingProject.Tests.Steps
{
    [Binding]
    public class DeleteCustomerSteps
    {
        private LoginPage _loginPage = null!;
        private ManagerPage _managerPage = null!;

        [Given(@"I navigate to the banking project page ""(.*)""")]
        public void GivenINavigateToTheBankingProjectPage(string url)
        {
            _loginPage = new LoginPage(Hooks.Hooks.Driver);
            _loginPage.NavigateTo(url);
        }

        [When(@"I log in as Bank Manager")]
        public void WhenILogInAsBankManager()
        {
            _managerPage = _loginPage.LoginAsBankManager();
        }

        [When(@"I open the Customers tab")]
        public void WhenIOpenTheCustomersTab()
        {
            _managerPage.OpenCustomersTab();
        }

        [When(@"I search for customer ""(.*)""")]
        public void WhenISearchForCustomer(string name)
        {
            _managerPage.SearchCustomer(name);
            System.Threading.Thread.Sleep(2000);
        }

        [When(@"I delete customer ""(.*)""")]
        public void WhenIDeleteCustomer(string name)
        {
            bool deleted = _managerPage.DeleteCustomer(name);
            Assert.That(deleted, Is.True, $"Customer \"{name}\" was not found in the table");

        }

        [Then(@"the customer ""(.*)"" should no longer appear in the customer list")]
        public void ThenTheCustomerShouldNoLongerAppearInTheCustomerList(string name)
        {
            Assert.That(_managerPage.IsCustomerPresent(name), Is.False,
                $"Customer \"{name}\" is still present in the list — delete did not work");
        }
    }
}