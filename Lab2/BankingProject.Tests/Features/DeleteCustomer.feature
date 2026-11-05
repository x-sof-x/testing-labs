Feature: Delete Customer in Banking System
  As a Bank Manager
  I want to be able to delete a customer
  So that I can keep the customer database clean

  Scenario: Successfully delete an existing customer
    Given I navigate to the banking project page "https://www.globalsqa.com/angularJs-protractor/BankingProject"
    When I log in as Bank Manager
    And I open the Customers tab
    And I search for customer "Ron"
    And I delete customer "Ron"
    Then the customer "Ron" should no longer appear in the customer list