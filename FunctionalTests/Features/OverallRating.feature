Feature: OverallRating
	As a user of Buggy Cars website
	I want to be able to view cars overall rating
	So that I can view cars details

@overallRatings
Scenario Outline: Navigate through multiple pages
	Given I go to overall ratings page
	When I select make <make> and model <model>
	Then I navigate to the <make> <model> page

	Examples:
		| make        | model  |
		| Lamborghini | Diablo |
		| Bugatti     | Veyron |