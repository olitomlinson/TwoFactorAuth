Feature: codes_dont_cross_pollinate
	As a PO/Customer
	I want 2fa codes to be isolated to an account
	So that multiple codes can be sent to the same phone number for different security use cases

Background:
	Given Benedict has a telephone number of 08798111555
	And Benedict has never been sent a two-factor-auth Sms before

	Given The customer Specsavers uses the two-factor-auth solution to secure their CRM service
	And Specsavers CRM service uses the Esendex account EX000001 for sending Sms
	And Specsavers CRM service requires the default code policy
	And Benedict is required to complete two-factor auth to use the Specsavers CRM service

	Given The customer CapitalOne uses the two-factor-auth solution to secure their BankingApp service
	And CapitalOne BankingApp service uses the Esendex account EX000002 for sending Sms
	And CapitalOne BankingApp service requires the default code policy
	And Benedict is required to complete two-factor auth to use the CapitalOne BankingApp service

Scenario: the correct code can be validated successfully, even when the user has received a code for a different service
	Given Benedict attempted to log in to the Specsavers CRM Service
	And Benedict was sent the code 123123 in an Sms at noon today
	And Benedict attempted to log in to the CapitalOne BankingApp Service
	And Benedict was sent the code 888888 in an Sms at noon today
	When Benedict supplies the first code 123123 to access the BankingApp service 1 minutes later
	Then the first code fails validation

Scenario: codes can't be used to validate other services, even if its for the same mobile number
	Given Benedict attempted to log in to the Specsavers CRM Service
	And Benedict was sent the code 123123 in an Sms at noon today
	And Benedict attempted to log in to the CapitalOne BankingApp Service
	And Benedict was sent the code 888888 in an Sms at noon today
	When Benedict supplies the first code 888888 to access the BankingApp service 1 minutes later
	Then the first code validates successfully



