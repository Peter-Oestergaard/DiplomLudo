Feature: LudoGame
In order to play Ludo
As a player
I want to move my pieces on the game board


	Scenario: User rolls a 5 and there is an opponent's piece on the destination tile
		Given a Ludo game board
		And a Red player with a piece at tile 0
		And a Blue player with a piece at tile 4
		When the Red player rolls a 5
		Then the Red piece should move to tile 5
		And the Blue piece should return to its home