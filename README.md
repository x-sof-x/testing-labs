# testing-labs

Лабораторні роботи з курсу «Автоматизовані системи тестування програмного продукту».

**Автор:** Софія Кононова, група ПМ-33, Національний університет «Львівська політехніка».

## Лабораторні роботи

| № | Тема | Технології | Папка |
|---|---|---|---|
| 1 | Модульне тестування ПЗ: data-driven тести методу `RunEstimate()` калькулятора | C#, MSTest, MS SQL Server (LocalDB) | [Lab1/Calculator.test](Lab1/Calculator.test) |
| 2 | Web тестування з використанням Selenium: сценарій Delete Customer | C#, Selenium WebDriver, SpecFlow, NUnit, Page Object | [Lab2/BankingProject.Tests](Lab2/BankingProject.Tests) |

## Лабораторна робота №1

Модульне тестування методу `RunEstimate()`, який обчислює арифметичний вираз у зворотному польському записі. Тестові дані зберігаються в таблиці `RunEstimateTestCases` бази `CalcTestsDB`, а скрипт створення бази лежить у `init_db.sql`. Тести виконуються параметризовано через `[DynamicData]`.

Детальніше: [Lab1/Calculator.test/README.md](Lab1/Calculator.test/README.md)

## Лабораторна робота №2

Автоматизоване тестування сайту [Banking Project](https://www.globalsqa.com/angularJs-protractor/BankingProject): вхід як Bank Manager, пошук і видалення клієнта «Ron» та перевірка, що клієнт зник зі списку. Поведінка описана мовою Gherkin, кроки реалізовані через SpecFlow, а сторінки винесені в Page Objects (`LoginPage`, `ManagerPage`).

Детальніше: [Lab2/BankingProject.Tests/README.md](Lab2/BankingProject.Tests/README.md)

## Структура репозиторію

```
testing-labs/
├── Lab1/
│   └── Calculator.test/       # лабораторна робота №1
└── Lab2/
    └── BankingProject.Tests/  # лабораторна робота №2
```

## Запуск

Кожна лабораторна робота відкривається у Visual Studio за власним файлом `.sln`. Порядок запуску описаний у README відповідної папки.
