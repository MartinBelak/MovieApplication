Feature: AddMovieToWishlist
	Add a movie from movie list to a user specific wishlist

Background: 
	Given the user is logged in his account
		| Username | Password |
		| Noro     | Noro     |
	And the user navigates to the Latest Movies page

Scenario: Movie is not in the wishlist
	When the user selects a movie which is not in his wishlist already
	And clicks the Add to Wishlist button
	Then a confirmation message "Movie added to wishlist." should appear
	And the movie can be seen in his wishlist

Scenario: Movie is already in wishlist
	When the user selects a movie which is in his wishlist already
	And clicks the Add to Wishlist button
	Then an error message "Movie is already in your wishlist." appears 
	