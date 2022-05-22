Feature: Registration
	As a user of Buggy Cars website
	I want to successfully register my details
	So that it can be used to log in

Background:
	Given I navigate to registration page

@registration
Scenario: Register with valid data
	Given I enter user details
		| Username                 | FirstName  | LastName | Password      | ConfirmPassword |
		| Generate Random Username | Automation | Test     | Password1234! | Password1234!   |
	When I click register
	Then I will be registered successfully
	* I can login using the registered login credentials
	* I should be successfully logged in

@registration
Scenario: Register an existing user
	Given I enter user details
		| Username   | FirstName | LastName | Password      | ConfirmPassword |
		| Automation | Test      | Doe      | Password1234! | Password1234!   |
	When I click register
	Then I get user already exist error

@registration
Scenario: Register with mismatched passwords
	Given I enter user details
		| Username                 | FirstName | LastName | Password      | ConfirmPassword |
		| Generate Random Username | John      | Doe      | Password1234! | Password4567!   |
	When I click register
	Then I get passwords mismatch error