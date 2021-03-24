Feature: RegisterUser
	Register a new user into the application

Scenario: Successful registration
	Given the user navigates to the webpage
	And clicks the register button
	When the user submits the correct information
	And saves the registration form
	Then the user is navigated to the login page
	And a confirmation message "Registration Succesfull :)" appears