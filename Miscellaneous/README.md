```mermaid
---
title: Ludo
---
classDiagram
    direction LR
    class Game{
        +GameBoard Board
        +Player CurrentPlayer
        +IDie Die
        -bool _dieRolled
        +Game(List&lt;Player>, IDie?)
        +StartingPlayer(Player)
        +RollDie()
        +PiecesWithLegalMoves() List~Piece~
        +NextTile(Piece) Tile?
    }
    class GameBoard{
        +Dictionary&lt;Color, Home> Homes
        -List~Tile?~ _mainTiles
        -int _numberOfMainTiles
        +Dictionary&lt;Color, Tile?> StartingTiles
        -Dictionary&lt;Color, int> _startingTilesIndices
        +GameBoard()
        +PutPlayerPiecesInHome(Player)
        +IsPieceMovable(Piece, int) bool
    }
    class Home{
        +List~Tile~ Tiles
        +Home()
        +AddPiece(Piece)
    }
    class Tile{
        +Piece? Piece
    }
    class Piece{
        +Color Color
        +bool IsHome
        +Piece(Color)
    }
    class Player{
        +Color Color
        +List~Piece~ Pieces
        +Player(Color)
    }
    class Color{
        <<Enumeration>>
        Red
        Green
        Yellow
        Blue
    }
    class Die{
        
    }
    class IDie{
        <<interface>>
        int Value
        Roll()
    }
    
    Die --|> IDie
    
    Game "1" --> "1" GameBoard
    Game "1" --> "1" IDie
    GameBoard "1" --> "4" Home
    Home "1" --> "4" Tile
    Tile "1" --> "0..1" Piece
    
    Player "*" --> "1" Color
    Player "1" --> "4" Piece
    Piece "*" --> "1" Color
```

```
    1  2  3  4  5  6  7  8  9 10 11 12 13 14 15
                    +--+--+--+
a                   |10|11|12|
                    +--+--+--+  +--+--+
b                   | 9|g0|13|<-| G| G|
                    +--+--+--+  +--+--+
c    +--+--+        | 8|g1|14|  | G| G|
     | R| R|        +--+--+--+  +--+--+
d    +--+--+        | 7|g2|15|
     | R| R|        +--+--+--+
e    +--+--+        | 6|g3|16|
       |            +--+--+--+
f      V            | 5|g4|17|
  +--+--+--+--+--+--+  +--+  +--+--+--+--+--+--+
g |51| 0| 1| 2| 3| 4 \ |g5| / 18|19|20|21|22|23|
  +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
h |50|r0|r1|r2|r3|r4|r5|  |y5|y4|y3|y2|y1|y0|24|
  +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
i |49|48|47|46|45|44 / |b5| \ 30|29|28|27|26|25|
  +--+--+--+--+--+--+  +--+  +--+--+--+--+--+--+
j                   |43|b4|31|            ^
                    +--+--+--+            |
k                   |42|b3|32|        +--+--+
                    +--+--+--+        | Y| Y|
l                   |41|b2|33|        +--+--+
           +--+--+  +--+--+--+        | Y| Y|
m          | B| B|  |40|b1|34|        +--+--+
           +--+--+  +--+--+--+
n          | B| B|->|39|b0|35|
           +--+--+  +--+--+--+
o                   |38|37|36|
                    +--------+
```

# Definitons

| Term                          | Meaning                                                                                                                                            |
|-------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------|
| Game board                    | he playing surface for Ludo, which is typically square in shape and divided into colored sections                                                  |
| Spaces or tiles               | The individual squares on the game board where players move their tokens                                                                           |
| Tokens or pawns               | The playing pieces used to represent each player's position on the game board. In Ludo, each player typically has four tokens of the same color    |
| Home or house                 | The area of the game board where each player's tokens start at the beginning of the game                                                           |
| Starting point or entry point | The first tile outside a player's house where their tokens enter the board                                                                         |
| Finish line                   | The last row of spaces on the game board that players must move their tokens to in order to win the game                                           |
| Dice                          | The small cubes used to determine the number of spaces a player can move their token on their turn                                                 |
| Barriers or blockades         | The obstacles that prevent a player's token from moving forward, which can be created by other players' tokens or certain spaces on the game board |
| Safe zones or home stretch    | The final row of spaces before a player's tokens reach the finish line, where their tokens are safe from being captured by other players' tokens   |
