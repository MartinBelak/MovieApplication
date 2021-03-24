Feature: RemoveMovieFromWishlist
	Remove a movie from users wishlist

Background:
	Given the user is logged in his account
		| Username | Password |
		| Noro     | Noro     |
	And the user navigates to the Wishlist page

Scenario: Remove movie from wishlist
	When the user selects a movie Stairs from his wishlist
	And clicks the Remove from Wishlist button
	Then a confirmation message "Movie was removed from your wishlist." should appear
	And the movie Stairs is removed from his wishlist