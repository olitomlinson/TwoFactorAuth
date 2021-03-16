Feature: retry_based_validation
	As a user who is trying to complete the two factor auth process
	I want to be able to validate my code multiple times
	So that If I dont get it correct, eventually I will get it correct and get access to my account

	As a two factor auth provider
	I want to restrict the amount of retries to 3 per code
	So that the code can't be brute force cracked

Background:
	Given The customer Specsavers uses the two-factor-auth solution to secure their CRM service
	And Specsavers CRM service uses the Esendex account EX000001 for sending Sms
	And Specsavers CRM service requires the default code policy
	And Benedict has a telephone number of 08798111444
	And Benedict is required to complete two-factor auth to use the Specsavers CRM service
	And Benedict has never been sent a two-factor-auth Sms before

@mytag
Scenario: validates successfully on the second attempt
	Given Benedict attempted to log in to the Specsavers CRM Service
	And Benedict was sent the code 999999 in an Sms at noon today
	When Benedict supplies the first code 111111 for validation 2 minutes later
	And Benedict supplies the second code 999999 for validation 1 minutes later
	Then the first code fails validation
	And the second code validates successfully

Scenario: validates successfully on the third attempt
	Given Benedict attempted to log in to the Specsavers CRM Service
	And Benedict was sent the code 999999 in an Sms at noon today
	When Benedict supplies the first code 111111 for validation 2 minutes later
	And Benedict supplies the second code 222222 for validation 1 minutes later
	And Benedict supplies the third code 999999 for validation 1 minutes later
	Then the first code fails validation
	And the second code fails validation
	And the third code validates successfully

Scenario: invalid on the 4th attempt even when code is correct
	Given Benedict attempted to log in to the Specsavers CRM Service
	And Benedict was sent the code 999999 in an Sms at noon today
	When Benedict supplies the first code 111111 for validation 2 minutes later
	And Benedict supplies the second code 222222 for validation 1 minutes later
	And Benedict supplies the third code 333333 for validation 1 minutes later
	And Benedict supplies the forth code 999999 for validation 1 minutes later
	Then the first code fails validation
	And the second code fails validation
	And the third code fails validation
	And the forth code fails validation