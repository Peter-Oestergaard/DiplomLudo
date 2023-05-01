Feature: LudoGame


    Scenario: Add a player to the game
        Given I have a Ludo game
        When I add a player with color "Red"
        Then The player should be added to the game
        
    Scenario: Cannot add the same player twice
        Given I have a Ludo game
        And I have added a player with color "Red"
        When I try to add the same player again
        Then The player should not be added twice
        
    Scenario: Cannot add more than 4 players
        Given I have a Ludo game with 4 players
        When I try to add another player with color "Red"
        Then The game should not allow more than 4 players
        
    Scenario: Move a player's piece
        Given I have a Ludo game with 2 players
        And the red player rolls a "3"
        When the red player moves their piece
        Then the piece should be at position "3"
        
    Scenario: Player wins when their piece reaches position 40
        Given I have a Ludo game with 2 players
        And the red player has their piece at position "37"
        And the red player rolls a "3"
        When the red player moves their piece
        Then the red player should win the game