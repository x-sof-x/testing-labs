# Лабораторна робота №2. Web тестування з використанням Selenium

Курс: «Автоматизовані системи тестування програмного продукту».

Автоматизоване тестування сайту [Banking Project](https://www.globalsqa.com/angularJs-protractor/BankingProject) з використанням Selenium WebDriver, шаблону Page Object та BDD (SpecFlow).

**Варіант сценарію:** вхід Bank Manager Login, операція **Delete Customer**.

## Сценарій (Gherkin)

```gherkin
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
```

## Технології

- C#, NUnit
- SpecFlow 3.9.0
- Selenium WebDriver (Google Chrome)

## Структура проєкту (`BankingProject.Tests`)

| Папка | Призначення |
|---|---|
| `Features` | файли специфікації `.feature` та згенерований код SpecFlow |
| `Pages` | класи Page Object: `LoginPage`, `ManagerPage` |
| `Steps` | реалізація кроків сценарію: `DeleteCustomerSteps` |
| `Hooks` | запуск і закриття браузера перед/після кожного сценарію |

## Запуск

1. Встановити Visual Studio та браузер Google Chrome.
2. Відкрити `BankingProject.Tests/BankingProject.Tests.sln` і відновити NuGet-пакети.
3. Зібрати проєкт.
4. Запустити тест через **Test Explorer**. Відкриється Chrome, і сценарій виконається автоматично.