Feature: PopularModel
	As a user of Buggy Cars website
	I want to be able to view popular model car
	So that I can vote for it

@vote
Scenario: Vote for popular model
	Given I navigate to registration page
	* I enter user details
		| Username                 | FirstName  | LastName | Password      | ConfirmPassword |
		| Generate Random Username | Automation | Test     | Password1234! | Password1234!   |
	* I click register
	* I will be registered successfully
	* I can login using the registered login credentials
	* I should be successfully logged in
	When I go to popular model page
	Then I can vote for popular model with comment This car is amazing
	And I verify my vote is recorded