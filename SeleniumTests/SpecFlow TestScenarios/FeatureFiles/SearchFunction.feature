Feature: SearchFunction
	Tests for search feature: search movie title, genre and actor

Background: 
	Given the user navigates to the Latest Movies page

Scenario: Search for a specific movie title	
	When the user enters Hamilton in the search field
	And selects Title from a dropdown menu
	And presses the Search! button
	Then a movie Hamilton should be displayed

Scenario: Search for a specific genre
	When the user enters Drama in the search field
	And selects Genre from a dropdown menu
	And presses the Search! button
	Then movies with the genre Drama should be displayed

Scenario: Search for a specific actor
	When the user enters Nicolas Cage in the search field
	And selects Actors from a dropdown menu
	And presses the Search! button
	Then movies with the actor Nicolas Cage should be displayed