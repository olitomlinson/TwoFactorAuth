Feature: simple_validate

Background: 
	Given The customer Specsavers uses the two-factor-auth solution to secure their CRM service
	And Specsavers CRM service uses the Esendex account EX000001 for sending Sms
	And Specsavers CRM service requires the default code policy
	And Malcom has a telephone number of 08798111222
	And Malcom is required to complete two-factor auth to use the Specsavers CRM service
	And Malcom has never been sent a two-factor-auth Sms before

Scenario: code is sent and validation fails
	Given Malcom attempted to log in to the Specsavers CRM Service
	And Malcom was sent the code 456345 in an Sms at noon today
	When Malcom supplies the first code 64351xc for validation 9 minutes later
	Then the first code fails validation

Scenario: code is sent and validated successfully
	Given Malcom attempted to log in to the Specsavers CRM Service
	And Malcom was sent the code 123123 in an Sms at noon today
	When Malcom supplies the first code 123123 for validation 9 minutes later
	Then the first code validates successfully

