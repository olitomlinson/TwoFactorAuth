Feature: time_based_validation
	As a two factor auth provider
	I want to prevent codes from living forever
	So that they are not a security vunerability

Background:
	Given The customer Specsavers uses the two-factor-auth solution to secure their CRM service
	And Specsavers CRM service uses the Esendex account EX000001 for sending Sms
	And Specsavers CRM service requires the default code policy
	And Benedict is required to complete two-factor auth to use the Specsavers CRM service
	And Benedict has a telephone number of 08798111333
	And Benedict has never been sent a two-factor-auth Sms before

Scenario: validation fails when the expirey date is exceeded
	Given Benedict was sent the code 456345 in an Sms at noon today
	When Benedict supplies the first code 111111 for validation 11 minutes later
	Then the first code fails validation

Scenario: validation fails when the expirey date is exceeded, even if the code is correct
	Given Benedict was sent the code 456345 in an Sms at noon today
	When Benedict supplies the first code 456345 for validation 11 minutes later
	Then the first code fails validation