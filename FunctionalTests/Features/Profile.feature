Feature: Profile
	As a user of Buggy Cars website
	I want to be able to login to my profile
	So that I can change my details

@profile
Scenario Outline: Change profile details
	Given I enter username john and password Password123!
	And I click login
	And I should be successfully logged in
	When I update profile details FirstName: <FirstName>; LastName: <LastName>; Gender: <Gender>; Age: <Age>; Address: <Address>; Phone: <Phone>; Hobby: <Hobby>
	And Click save
	Then My profile should be updated

	Examples:
		| FirstName  | LastName | Gender | Age                 | Address                      | Phone     | Hobby  |
		| Automation |          |        |                     |                              |           |        |
		|            | Test     |        |                     |                              |           |        |
		|            |          | Male   |                     |                              |           |        |
		|            |          |        | Generate Random Age |                              |           |        |
		|            |          |        |                     | 123 Express Street, Auckland |           |        |
		|            |          |        |                     |                              | 123456789 |        |
		|            |          |        |                     |                              |           | Hiking |

@profile @logout
Scenario: logout
	Given I enter username AutomationTest and password Password1234!
	And I click login
	And I should be successfully logged in
	When I click logout
	Then I should be logged out successfully