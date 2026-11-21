@"
# Лабораторна робота №3 — Тестування Web API

**Папка проєкту:** `./WebAPI.test`

## Мета роботи
Закріпити на практиці автоматизоване тестування Web API із використанням бібліотеки **RestSharp** та підходу **BDD** (SpecFlow + NUnit)[cite: 1].

## Технології та інструменти
* **Платформа:** .NET 8 (C#)[cite: 1]
* **Тестовий фреймворк:** NUnit[cite: 1]
* **BDD:** SpecFlow (мова сценаріїв Gherkin)[cite: 1]
* **HTTP-клієнт:** RestSharp[cite: 1]

## Реалізовані тести
1. **Restful-Booker API** (`https://restful-booker.herokuapp.com`)[cite: 1]:
   * Отримання токена авторизації (`POST /auth`) у блоці `Background`[cite: 1].
   * Повний цикл CRUD-операцій для бронювань (`POST`, `GET`, `PUT`, `DELETE`)[cite: 1].
   * Негативний сценарій: перевірка заборони видалення бронювання без токена (`403 Forbidden`)[cite: 1].
2. **JSONPlaceholder API** (`https://jsonplaceholder.typicode.com`)[cite: 1]:
   * Перевірка CRUD-операцій над ресурсом `/posts` (`POST`, `GET`, `PUT`, `PATCH`, `DELETE`)[cite: 1].
   * Фільтрація постів за параметром запиту (`GET /posts?userId=1`)[cite: 1].
   * Параметризоване тестування (`Scenario Outline`) та негативний тест на неіснуючий ресурс (`404 Not Found`)[cite: 1].

## Структура проєкту
* `Features/` — сценарії тестування мовою Gherkin (`.feature`)[cite: 1, 2]
* `StepDefinitions/` — реалізація кроків тестування на C# (`BookerSteps.cs`, `PostsSteps.cs`)[cite: 1, 2]
* `Support/` — класи контексту для передачі стану між кроками (`ApiContext.cs`, `PostsContext.cs`)[cite: 1, 2]

## Запуск тестів
```bash
cd WebAPI.test
dotnet test