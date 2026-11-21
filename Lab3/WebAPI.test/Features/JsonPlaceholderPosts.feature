Feature: JSONPlaceholder posts CRUD
  As an API client
  I want to manage posts
  So that I can create, read, update and delete them

  Scenario: Create a new post
    When I create a post with title "My title" and body "My body" for user 1
    Then the JSONPlaceholder status code should be 201
    And the response field "title" should be "My title"
    And the response field "body" should be "My body"
    And the response number "userId" should be 1
    And the response number "id" should be 101

  Scenario: Read a single post
    When I get the post with id 1
    Then the JSONPlaceholder status code should be 200
    And the response number "id" should be 1
    And the response should contain fields "userId, id, title, body"

  Scenario: Read all posts
    When I get all posts
    Then the JSONPlaceholder status code should be 200
    And the response array should contain 100 items

  Scenario: Update a post with PUT
    When I replace the post 1 with title "Updated title" and body "Updated body" for user 1
    Then the JSONPlaceholder status code should be 200
    And the response number "id" should be 1
    And the response field "title" should be "Updated title"
    And the response field "body" should be "Updated body"

  Scenario: Partially update a post with PATCH
    When I patch the post 1 with title "Patched title"
    Then the JSONPlaceholder status code should be 200
    And the response number "id" should be 1
    And the response field "title" should be "Patched title"
    And the response should contain fields "userId, body"

  Scenario: Delete a post
    When I delete the post 1
    Then the JSONPlaceholder status code should be 200
    And the response body should be an empty object

  Scenario: Get a non-existent post
    When I get the post with id 99999
    Then the JSONPlaceholder status code should be 404

  Scenario: Filter posts by user
    When I get posts of user 1
    Then the JSONPlaceholder status code should be 200
    And every post in the response should have userId 1

  Scenario Outline: Read several posts by id
    When I get the post with id <id>
    Then the JSONPlaceholder status code should be 200
    And the response number "id" should be <id>

    Examples:
      | id |
      | 1  |
      | 50 |
      | 100 |