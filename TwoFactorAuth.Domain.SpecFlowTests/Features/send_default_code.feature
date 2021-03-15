Feature: send_default_code
	As a basic customer for the 2fa product
	I want to send 2fa codes in the simpliest way to my users
	So that I can secure my services in a standard

Background:
	Given The customer Specsavers uses the two-factor-auth solution to secure their CRM service
	And Specsavers CRM service uses the Esendex account EX000001 for sending Sms
	And Specsavers CRM service requires the default code policy
	And Jenny has a telephone number of 01992123123
	And Jenny is required to complete two-factor auth to use the Specsavers CRM service

Scenario: default send 
	Given Jenny attempted to log in to the Specsavers CRM Service
	When Specsavers CRM service intiates the two factor authorization process for Jenny
	Then Jenny receives an Sms
	And the code that Jenny received is 6 characters long
	And the code that Jenny received only contains numbers
