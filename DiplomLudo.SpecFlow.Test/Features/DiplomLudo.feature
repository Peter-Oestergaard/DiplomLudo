Feature: DiplomLudo

Scenario Outline: Starting a new game with two players
	Given the first player is <color1>
	And the second player is <color2>
	When the game is started
	Then <color1> home will have 4 <color1> pieces
	And <color2> home will have 4 <color2> pieces
	
	Examples:
	| color1 | color2 |
	| red    | yellow |
	| green  | blue   |
	
@RequiresCheatingDie
Scenario: The first player begins the game with a die roll
	Given a two player game in it's initial state with players red and blue
	And red is the current player
	When the current player rolls a 6 with the die
	Then red can move any of their pieces to the red starting tile
	
Scenario: A player moves a piece to starting point with another players piece on it
	Given a game with reds pieces at home
	And one of yellows pieces on reds starting tile
	When red moves a piece to reds starting tile
	Then yellows piece is returned to yellows home
	
	
	Scenario: User rolls a 5 and there is an opponent's piece on the destination tile
		Given a Ludo game board
		And a Red player with a piece at tile 0
		And a Blue player with a piece at tile 4
		When the Red player rolls a 5
		Then the Red piece should move to tile 5
		And the Blue piece should return to its home