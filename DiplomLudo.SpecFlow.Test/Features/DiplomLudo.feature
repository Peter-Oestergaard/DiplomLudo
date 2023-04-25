Feature: DiplomLudo
	
/*
	Game board: The playing surface for Ludo, which is typically square in shape and divided into colored sections.
	Spaces or tiles: The individual squares on the game board where players move their tokens.
	Tokens or pawns: The playing pieces used to represent each player's position on the game board. In Ludo, each player typically has four tokens of the same color.
	Home or house: The area of the game board where each player's tokens start at the beginning of the game.
	Starting point or entry point: The first tile outside a player's house where their tokens enter the board.
	Finish line: The last row of spaces on the game board that players must move their tokens to in order to win the game.
	Dice: The small cubes used to determine the number of spaces a player can move their token on their turn.
	Barriers or blockades: The obstacles that prevent a player's token from moving forward, which can be created by other players' tokens or certain spaces on the game board.
	Safe zones or home stretch: The final row of spaces before a player's tokens reach the finish line, where their tokens are safe from being captured by other players' tokens.
*/

@startup
Scenario: Starting a new game with red and yellow
	Given the number of players is 2
	And the first player is yellow
	And the second player is red
	When the game is started
	Then red home will have 4 red pieces
	And yellow home will have 4 yellow pieces
	
Scenario: The first player begins the game with a die roll
	Given a game in it's initial state with red and yellow players
	And red is the current player
	When red rolls a 6 with the die
	Then red can move any of their pieces to the red starting tile
	
Scenario: A player moves a piece to starting point with another players piece on it
	Given a game with reds pieces at home
	And one of yellows pieces on reds starting tile
	When red moves to starting point
	Then yellows piece is returned to yellow home