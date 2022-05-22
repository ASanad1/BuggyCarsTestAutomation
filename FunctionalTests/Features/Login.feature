Feature: Login
	As a user of Buggy Cars website
	I want to be able to login
	So that I can access my account

@login
Scenario: Login with valid credentials
	Given I enter username AutomationTest and password Password1234!
	When I click login
	Then I should be successfully logged in

@login
Scenario: Login with invalid credentials
	Given I enter username notRegisteredUserName and password Password123!
	When I click login
	Then I get invalid credentials error