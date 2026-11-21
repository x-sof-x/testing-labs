Feature: Restful-Booker booking CRUD
  As an API client
  I want to manage bookings
  So that I can create, read, update and delete reservations

  Background:
    Given I have an auth token

  Scenario: Create a booking
    When I create a booking for "John" "Smith" with total price 150
    Then the response status code should be 200
    And the response should contain a booking id

  Scenario: Read a booking
    Given a booking exists for "Anna" "Brown" with total price 200
    When I get the booking by id
    Then the response status code should be 200
    And the booking firstname should be "Anna"

  Scenario: Update a booking
    Given a booking exists for "Anna" "Brown" with total price 200
    When I update the booking with firstname "Maria" and total price 300
    Then the response status code should be 200
    And the booking firstname should be "Maria"

  Scenario: Delete a booking
    Given a booking exists for "Anna" "Brown" with total price 200
    When I delete the booking
    Then the response status code should be 201
    When I get the booking by id
    Then the response status code should be 404

  Scenario: Delete a booking without token is forbidden
    Given a booking exists for "Anna" "Brown" with total price 200
    When I delete the booking without a token
    Then the response status code should be 403