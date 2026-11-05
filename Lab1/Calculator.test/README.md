# Калькулятор: Data-Driven Testing (MSTest + MS SQL Server)

Цей репозиторій містить проєкт калькулятора та реалізацію модульного тестування за методологією **Data-Driven Testing (DDT)** для методу обчислення виразів `AnalaizerClass.RunEstimate()`.

Тестові набори зберігаються у реляційній базі даних **MS SQL Server (LocalDB)** та динамічно вичитуються через атрибут `[DataSource]` під час виконання тестів у середовищі Visual Studio.

---

##  Вимоги до середовища

Для запуску проєкту та тестів необхідно мати:
* **Visual Studio**  із робочим навантаженням:
  * *.NET desktop development*
  * *Data storage and processing* (SQL Server Data Tools / SSDT)
* **SQL Server Express LocalDB** (за замовчуванням постачається разом із Visual Studio під назвою `(localdb)\MSSQLLocalDB`).

---

##  Покрокова інструкція із запуску

### 1. Клонування репозиторію
Склонуйте проєкт на свій комп'ютер через термінал:
bash

# 2. Ініціалізація бази даних тестів
Відкрийте файл init_db.sql (знаходиться в корені репозиторію) через Visual Studio або SSMS.

Підключіться до локального сервера:

Server name: (localdb)\MSSQLLocalDB

Authentication: Windows Authentication

Виконайте скрипт (Ctrl + Shift + E).

Переконайтеся, що створено базу CalcTestsDB, таблицю dbo.RunEstimateTestCases та вставлено тестові набори.

# 3. Відкриття та збірка рішення
Відкрийте файл рішення Calculator_Exam_CommandProject.sln у Visual Studio.

Перевірте рядок підключення у файлі тесту UnitTestProject1/UnitTest1.cs:

csharp
[DataSource(
    "System.Data.SqlClient",
    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CalcTestsDB;Integrated Security=True",
    "RunEstimateTestCases",
    DataAccessMethod.Sequential)]
⚠️ Якщо назва вашого екземпляра SQL Server відрізняється, вкажіть власне значення у Data Source.

Зберіть рішення комбінацією клавіш Ctrl + Shift + B (або через меню Build → Build Solution).

# 4. Запуск тестів
Відкрийте вікно тестів: Test → Windows → Test Explorer (Ctrl + E, T).

Натисніть Run All Tests (Ctrl + R, A).

Розгорніть вузол RunEstimate_DataDrivenTest, щоб переглянути результати виконання для кожного окремого рядка з бази даних.

# 5. Структура тестових даних
Таблиця dbo.RunEstimateTestCases містить такі поля:

TestId (INT, PK) — первинний ключ автоінкременту

TestName (NVARCHAR) — короткий опис тестового випадку (пріоритет операцій, ділення на нуль тощо)

InputExpression (NVARCHAR) — вхідний математичний рядок для аналізатора

ExpectedResult (NVARCHAR) — очікуване обчислене число або код помилки (наприклад, &09)

IsErrorExpected (BIT) — ознака помилки (1 — очікується помилка, 0 — успішний розрахунок)

#  Підсумок
Цей проєкт демонструє використання Data-Driven Testing у MSTest із джерелом даних у MS SQL Server LocalDB, що дозволяє централізовано зберігати тестові набори та автоматично перевіряти метод RunEstimate() на різних виразах.
