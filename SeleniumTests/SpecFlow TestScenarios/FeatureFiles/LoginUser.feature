Feature: LoginUser
	A test to login user into the application

Background: 
	Given the user navigates to the webpage

Scenario: Successful Login
	When the user enters correct login information
		| Username | Password |
		| Noro     | Noro     |
	Then the user should be successfully logged in and navigated to main page

Scenario: Unsuccessful Login
	When the user enters incorrect login information
		| Username | Password |
		| Nori     | Noro     | 
	Then an error message "The username or password is not correct." should appear 