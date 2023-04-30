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
	Given a two player game in its initial state with players red and blue
	And red is the current player
	When the current player rolls a 6 with the die
	Then red can move any of their pieces to the red starting tile
	
@RequiresCheatingDie
Scenario: A player moves a piece to starting point with another players piece on it
	Given a two player game in its initial state with players red and yellow
	And one of yellows pieces is on reds starting tile
	And red is the current player
	When the current player rolls a 6 with the die
	And red moves a piece to reds starting tile
	Then yellows piece is returned to yellows home
	
@RequiresCheatingDie
Scenario Outline: If a player has all their pieces at home, roll a die a maximum
	of 3 times to roll a 6 before passing the turn
	Given a two player game in its initial state with players blue and green
	And green is the current player
	And doesn't roll a six until attempt <attempts>
	Then they will have rolled the die <attempts> times
	And have four legal moves to greens starting tile
	When green moves a piece to greens starting tile
	Then green will have 1 piece(s) on the green starting tile
	And it will be blues turn
	
	Examples:
	| attempts |
	| 1        |
	| 2        |
	| 3        |

@RequiresCheatingDie
Scenario Outline: A player moves into their home stretch
	Given a two player game in its initial state with players yellow and green
	And yellow is the current player
	And one of yellows pieces is <tiles to home stretch> tiles in front of the star before yellows home stretch
	When the current player rolls a <roll> with the die
	And current player moves that piece
	Then that piece is <tiles from finish> tiles away from the finish tile
	
	Examples:
	| tiles to home stretch | roll | tiles from finish |
	| 2                     | 5    | 3                 |
	| 5                     | 6    | 5                 |
	| 1                     | 6    | 1                 |

@RequiresCheatingDie
Scenario Outline: A player has a piece in the home stretch but rolls too many with the die
	and has to move away from finish
	Given a two player game in its initial state with players yellow and green
	And yellow is the current player
	And one of yellows pieces is <tiles to finish> tiles away from the finish tile
	When the current player rolls a <roll> with the die
	And current player moves that piece
	Then that piece is <tiles from finish> tiles away from the finish tile
	
	Examples:
	  | tiles to finish | roll | tiles from finish |
	  | 1               | 2    | 1                 |
	  | 2               | 6    | 4                 |
