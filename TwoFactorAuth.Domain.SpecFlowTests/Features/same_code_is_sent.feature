Feature: same_code_is_sent
	As an two factor auth provider
	I want to send the exact same code multiple times whilst the code is active
	So that if a network issue was to occur and messages were delayed / not delivered in order then the code would work

Background: 
	Given The customer Specsavers uses the two-factor-auth solution to secure their CRM service
	And Specsavers CRM service uses the Esendex account EX000001 for sending Sms
	And Specsavers CRM service requires the default code policy
	And Benedict has a telephone number of 08798111555
	And Benedict is required to complete two-factor auth to use the Specsavers CRM service
	And Benedict has never been sent a two-factor-auth Sms before

Scenario: Same code is sent again, whilst it has not expired
	Given Benedict attempted to log in to the Specsavers CRM Service
	And Benedict was sent the code 999999 in an Sms at noon today
	When Benedict requests a validation code 3 minutes later
	Then Benedict receives the validation code 999999

Scenario: A different code is sent again, after the expirey period
	Given Benedict attempted to log in to the Specsavers CRM Service
	And Benedict was sent the code 999999 in an Sms at noon today
	When Benedict requests a validation code 11 minutes later
	Then Benedict receives a validation code that is different to 999999
	

