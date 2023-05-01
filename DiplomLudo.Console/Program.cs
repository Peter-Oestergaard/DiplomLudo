using DiplomLudo;
using DiplomLudo.Console;
using System.Text;
using System.Text.RegularExpressions;

Console.OutputEncoding = Encoding.UTF8;

const string r = "\u001b[0;31m";
const string g = "\u001b[0;32m";
const string y = "\u001b[0;33m";
const string b = "\u001b[0;34m";
const string dr = "\u001b[2;31m";
const string dg = "\u001b[2;32m";
const string dy = "\u001b[2;33m";
const string db = "\u001b[2;34m";
const string w = "\u001b[0;37m";

const string gameBoardTemplate =
    "\n" +
    "{w}                  ┏━━┳━━┳━━┓\n" +
    "{w}                  ┃10┃11┃12┃\n" +
    "{w}                  ┣━━{g}╋━━╋━━┫  ┏━━┳━━┓ G\n" +
    "{w}                  ┃09{g}┃g0{g}┃13{g}┃◀━┃gw┃gx┃ r\n" +
    "{r}   Red            {w}┣━━{g}╋━━╋━━┫  ┣━━╋━━┫ e\n" +
    "{r}   ┏━━┳━━┓        {w}┃08{g}┃g1{g}┃{w}14┃  {g}┃gy┃gz┃ e\n" +
    "{r}   ┃rw┃rx┃        {w}┣━━{g}╋━━╋{w}━━┫  {g}┗━━┻━━┛ n\n" +
    "{r}   ┣━━╋━━┫        {w}┃07{g}┃g2{g}┃{w}15┃\n" +
    "{r}   ┃ry┃rz┃        {w}┣━━{g}╋━━╋{w}━━┫\n" +
    "{r}   ┗━━┻━━┛        {w}┃06{g}┃g3{g}┃{w}16┃\n" +
    "{r}     ┃            {w}┣━━{g}╋━━╋{w}━━┫\n" +
    "{r}     ▼            {w}┃05{g}┃g4{g}┃{w}17┃\n" +
    "{w}┏━━{r}┳━━┳{w}━━┳━━┳━━┳━━╋━━{g}╋━━╋{w}━━╋━━┳━━┳━━┳━━┳━━┳━━┓\n" +
    "{w}┃51{r}┃00{r}┃{w}01┃02┃03┃04┃██{g}┃g5{g}┃{w}██┃18┃19┃20┃21┃22┃23┃\n" +
    "{w}┣━━{r}╋━━╋━━╋━━╋━━╋━━╋━━{w}╋{g}━━{w}╋{y}━━╋━━╋━━╋━━╋━━╋━━╋{w}━━┫\n" +
    "{w}┃50{r}┃r0{r}┃r1{r}┃r2{r}┃r3{r}┃r4{r}┃r5{r}┃{w}██{y}┃y5{y}┃y4{y}┃y3{y}┃y2{y}┃y1{y}┃y0{y}┃{w}24┃\n" +
    "{w}┣━━{r}╋━━╋━━╋━━╋━━╋━━╋━━{w}╋{b}━━{w}╋{y}━━╋━━╋━━╋━━╋━━╋━━╋{w}━━┫\n" +
    "{w}┃49┃48┃47┃46┃45┃44┃██{b}┃b5{b}┃{w}██┃30┃29┃28┃27{y}┃26{y}┃{w}25┃\n" +
    "{w}┗━━┻━━┻━━┻━━┻━━┻━━╋━━{b}╋━━╋{w}━━╋━━┻━━┻━━┻━━{y}┻━━┻{w}━━┛\n" +
    "{w}                  {w}┃43{b}┃b4{b}┃{w}31┃        {y}    ▲\n" +
    "{w}                  {w}┣━━{b}╋━━╋{w}━━┫        {y}    ┃\n" +
    "{w}                  {w}┃42{b}┃b3{b}┃{w}32┃        {y}┏━━┳━━┓\n" +
    "{w}                  {w}┣━━{b}╋━━╋{w}━━┫        {y}┃yw┃yx┃\n" +
    "{w}                  {w}┃41{b}┃b2{b}┃{w}33┃        {y}┣━━╋━━┫\n" +
    "{b}       B ┏━━┳━━┓  {w}┣━━{b}╋━━╋{w}━━┫        {y}┃yy┃yz┃\n" +
    "{b}       l ┃bw┃bx┃  {w}┃40{b}┃b1{b}┃{w}34┃        {y}┗━━┻━━┛\n" +
    "{b}       u ┣━━╋━━┫  ┣━━╋━━╋{w}━━┫        {y}Yellow\n" +
    "{b}       e ┃by┃bz┃━▶┃39{b}┃b0{b}┃{w}35┃\n" +
    "{b}         ┗━━┻━━┛  ┣━━╋━━╋{w}━━┫\n" +
    "{w}                  {w}┃38┃37┃36┃\n" +
    "{w}                  {w}┗━━┻━━┻━━┛";

bool running = true;

// Get players
bool readyToPlay = false;
Dictionary<Color, Player> players = new();
HashSet<Color> availableColors = new()
{
    Color.Red,
    Color.Green,
    Color.Yellow,
    Color.Blue
};

string message = String.Empty;
do
{
    Console.Clear();
    Console.WriteLine("\n" + message);
    message = string.Empty;
    Console.WriteLine("\n    Choose players");

    foreach (Color color in availableColors)
    {
        Console.WriteLine($"      {(int) color + 1}. {Enum.GetName(color)}");
    }

    Console.WriteLine("\n    Players");

    foreach (Player player in players.Values)
    {
        Console.WriteLine($"      {Enum.GetName(player.Color)}");
    }

    Console.WriteLine("\n    Press <space> when ready to play");

    var input = Console.ReadKey(false);

    switch (input.Key)
    {
        case ConsoleKey.D1:
            if (players.ContainsKey(Color.Red))
            {
                message = "Red is already chosen. Choose another color";
                break;
            }
            players[Color.Red] = new Player(Color.Red);
            availableColors.Remove(Color.Red);
            break;
        case ConsoleKey.D2:
            if (players.ContainsKey(Color.Green))
            {
                message = "Green is already chosen. Choose another color";
                break;
            }
            players[Color.Green] = new Player(Color.Green);
            availableColors.Remove(Color.Green);
            break;
        case ConsoleKey.D3:
            if (players.ContainsKey(Color.Yellow))
            {
                message = "Yellow is already chosen. Choose another color";
                break;
            }
            players[Color.Yellow] = new Player(Color.Yellow);
            availableColors.Remove(Color.Yellow);
            break;
        case ConsoleKey.D4:
            if (players.ContainsKey(Color.Blue))
            {
                message = "Blue is already chosen. Choose another color";
                break;
            }
            players[Color.Blue] = new Player(Color.Blue);
            availableColors.Remove(Color.Blue);
            break;
        case ConsoleKey.Spacebar:
            if (players.Count < 2)
            {
                message = "    Need more players. Choose one of the available colors.";
                continue;
            }
            readyToPlay = true;
            break;
        default:
            message = "    Unknown key. Try again.";
            break;
    }
    if (players.Count == 4) readyToPlay = true;
} while (!readyToPlay);

// Begin game
Game game = new Game(players);
// Game game = new Game(players, new CheatingDie {cheat = () => 6});
game.StartingPlayer(players.ToList()[new Random().Next(0, players.Count)].Value);
// game.StartingPlayer(players[Color.Red]);

do
{
    Player currentPlayer = game.CurrentPlayer!;
    int dieRoll = game.RollDie();
    var moves = game.PiecesWithLegalMoves();

    // Reset gameboard
    string gameBoard = gameBoardTemplate;

    // Insert pieces on their tiles
    gameBoard = UpdateBoard(gameBoard);

    // Update possible moves
    gameBoard = UpdatePossibleMoves(gameBoard, moves, dieRoll);
    
    // Insert starts and globes
    gameBoard = UpdateBoardStartAndGlobes(gameBoard);

    // Clear unused board indices
    Regex tileContentIds = new(@"[rgybwxyz0-9]{2}");
    gameBoard = tileContentIds.Replace(gameBoard, "  ");

    // Insert terminal color codes
    var sb = new StringBuilder(gameBoard);
    sb = sb.Replace("{r}", r);
    sb = sb.Replace("{g}", g);
    sb = sb.Replace("{y}", y);
    sb = sb.Replace("{b}", b);
    sb = sb.Replace("{dr}", dr);
    sb = sb.Replace("{dg}", dg);
    sb = sb.Replace("{dy}", dy);
    sb = sb.Replace("{db}", db);
    gameBoard = sb.Replace("{w}", w).ToString();

    // Render
    Console.Clear();
    Console.WriteLine(gameBoard);
    string colorCode = $"{w}";
    switch (currentPlayer.Color)
    {
        case Color.Red:
            colorCode = $"{r}";
            break;
        case Color.Green:
            colorCode = $"{g}";
            break;
        case Color.Yellow:
            colorCode = $"{y}";
            break;
        case Color.Blue:
            colorCode = $"{b}";
            break;
    }
    Console.WriteLine($"\n Current player: {colorCode}{Enum.GetName(currentPlayer.Color)}{w}\n");
    RenderDie(dieRoll);

    // Dictionary<int, Piece> pieceChoice = new Dictionary<int, Piece>();
    // Dictionary<int, Tile> moveToRender = new Dictionary<int, Tile>();

    List<int> movablePiecesIndices = new List<int>();
    if (moves.Count > 0)
    {
        Console.WriteLine("\n    You can make a move.");
        Console.WriteLine("    Press the corresponding number to move that piece:");
        Console.Write($"        {currentPlayer.Pieces.IndexOf(moves[0]) + 1}");
        movablePiecesIndices.Add(currentPlayer.Pieces.IndexOf(moves[0]));
        for (int i = 1; i < moves.Count; i++)
        {
            Console.Write($", {currentPlayer.Pieces.IndexOf(moves[i]) + 1}");
            movablePiecesIndices.Add(currentPlayer.Pieces.IndexOf(moves[i]));
        }
        Console.WriteLine();
    }
    else if (game.CurrentPlayerNumberOfDieRolls == 3)
    {
        game.PassTurnToNextPlayer();
        Console.WriteLine("\n    Tough luck. Press any key to continue to next player.");
    }

    if (moves.Count == 0 && currentPlayer == game.CurrentPlayer)
    {
        Console.WriteLine("\n    Press any key to roll the die again.");
    }


    // Get player input
    bool validInput = false;
    do
    {
        var input = Console.ReadKey(false);
        if (movablePiecesIndices.Count > 0)
        {
            if (input.Key == ConsoleKey.D1)
            {
                // loop through values, if 0 is present then choose that
                // otherwise error
                for (int i = 0; i < movablePiecesIndices.Count; i++)
                {
                    if (movablePiecesIndices[i] == 0)
                    {
                        game.Move(currentPlayer.Pieces[0]);
                        validInput = true;
                    }
                }
                if (!validInput)
                {
                    Console.WriteLine("\n    Unknown move. Choose one of the listed moves.");
                    continue;
                }
            }
            else if (input.Key == ConsoleKey.D2)
            {
                // loop through values, if 1 is present then choose that
                // otherwise error
                for (int i = 0; i < movablePiecesIndices.Count; i++)
                {
                    if (movablePiecesIndices[i] == 1)
                    {
                        game.Move(currentPlayer.Pieces[1]);
                        validInput = true;
                    }
                }
                if (!validInput)
                {
                    Console.WriteLine("\n    Unknown move. Choose one of the listed moves.");
                    continue;
                }
            }
            else if (input.Key == ConsoleKey.D3)
            {
                // loop through values, if 2 is present then choose that
                // otherwise error
                for (int i = 0; i < movablePiecesIndices.Count; i++)
                {
                    if (movablePiecesIndices[i] == 2)
                    {
                        game.Move(currentPlayer.Pieces[2]);
                        validInput = true;
                    }
                }
                if (!validInput)
                {
                    Console.WriteLine("\n    Unknown move. Choose one of the listed moves.");
                    continue;
                }
            }
            else if (input.Key == ConsoleKey.D4)
            {
                // loop through values, if 3 is present then choose that
                // otherwise error
                for (int i = 0; i < movablePiecesIndices.Count; i++)
                {
                    if (movablePiecesIndices[i] == 3)
                    {
                        game.Move(currentPlayer.Pieces[3]);
                        validInput = true;
                    }
                }
                if (!validInput)
                {
                    Console.WriteLine("\n    Unknown move. Choose one of the listed moves.");
                    continue;
                }
            }
        }
        else
        {
            validInput = true;
        }
        if (input.Key == ConsoleKey.Escape
            || input.Key == ConsoleKey.Q)
        {
            running = false;
            validInput = true;
            continue;
        }
        
    } while (!validInput);
} while (running);

Environment.Exit(0);

// Functions
string UpdateBoard(string gameBoard)
{
    foreach (Player player in players.Values)
    {
        int homeIndex = -1;

        for (int i = 0; i < player.Pieces.Count; i++)
        {
            if (player.Pieces[i].Tile!.Type == TileType.Home)
            {
                homeIndex++;
                gameBoard = RenderPieceAtHome(player.Pieces[i], i + 1, homeIndex, gameBoard);
            }
            else
            {
                gameBoard = RenderPiece(player.Pieces[i], i + 1, gameBoard);
            }
        }
        // {
        //     switch (piece.Tile!.Type)
        //     {
        //         case TileType.Home:
        //             break;
        //     }
        // }
    }
    return gameBoard;
}

string UpdatePossibleMoves(string gameBoard, List<Piece> moves, int dieRoll)
{
    var sb = new StringBuilder(gameBoard);
    foreach (Piece piece in moves)
    {
        var next = game.NextTile(piece, dieRoll);
        string tileKeyColor = "";
        if (next.Type == TileType.Finish)
        {
            tileKeyColor += Enum.GetName(piece.Color)!.ToLower()[0];
        }
        tileKeyColor += next.Index;
        tileKeyColor = tileKeyColor.PadLeft(2, '0');
        sb.Replace(tileKeyColor, $"{{d{Enum.GetName(piece.Color)!.ToLower()[0]}}}● {{w}}");
    }
    string r = sb.ToString();
    return r;
}

string RenderPieceAtHome(Piece piece, int index, int indexInHome, string gameBoard)
{
    var sb = new StringBuilder(gameBoard);
    string tileKeyColor = "";
    switch (piece.Color)
    {
        case Color.Red:
            tileKeyColor += "r";
            break;
        case Color.Green:
            tileKeyColor += "g";
            break;
        case Color.Yellow:
            tileKeyColor += "y";
            break;
        case Color.Blue:
            tileKeyColor += "b";
            break;
    }

    string tileKey = tileKeyColor;
    switch (indexInHome)
    {
        case 0:
            tileKey += "w";
            sb.Replace(tileKey, $"●{index}");
            break;
        case 1:
            tileKey += "x";
            sb.Replace(tileKey, $"●{index}");
            break;
        case 2:
            tileKey += "y";
            sb.Replace(tileKey, $"●{index}");
            break;
        case 3:
            tileKey += "z";
            sb.Replace(tileKey, $"●{index}");
            break;
    }

    return sb.ToString();
}

string RenderPiece(Piece piece, int index, string gameBoard)
{
    var sb = new StringBuilder(gameBoard);

    var tile = piece.Tile!;
    string tileKeyColor = "";
    if (tile.Type == TileType.Finish)
    {
        tileKeyColor += Enum.GetName(piece.Color)!.ToLower()[0];
    }
    tileKeyColor += tile.Index;
    tileKeyColor = tileKeyColor.PadLeft(2, '0');
    sb.Replace(tileKeyColor, $"{{{Enum.GetName(piece.Color)!.ToLower()[0]}}}●{index}{{w}}");

    return sb.ToString();
}

string UpdateBoardStartAndGlobes(string gameBoard)
{
    var sb = new StringBuilder(gameBoard);

    string[] stars = {"05", "11", "18", "24", "31", "37", "44", "50"};
    string[] globes = {"00", "08", "13", "21", "26", "34", "39", "47"};

    foreach (string star in stars)
    {
        sb.Replace(star, "S ");
    }
    foreach (string globe in globes)
    {
        sb.Replace(globe, "G ");
    }

    return sb.ToString();
}

void RenderDie(int value)
{
    switch (value)
    {
        case 1:
            Console.WriteLine("\n        ┏━━━━━┓");
            Console.WriteLine("        ┃     ┃");
            Console.WriteLine("        ┃  ●  ┃");
            Console.WriteLine("        ┃     ┃");
            Console.WriteLine("        ┗━━━━━┛");
            break;
        case 2:
            Console.WriteLine("\n        ┏━━━━━┓");
            Console.WriteLine("        ┃●    ┃");
            Console.WriteLine("        ┃     ┃");
            Console.WriteLine("        ┃    ●┃");
            Console.WriteLine("        ┗━━━━━┛");
            break;
        case 3:
            Console.WriteLine("\n        ┏━━━━━┓");
            Console.WriteLine("        ┃●    ┃");
            Console.WriteLine("        ┃  ●  ┃");
            Console.WriteLine("        ┃    ●┃");
            Console.WriteLine("        ┗━━━━━┛");
            break;
        case 4:
            Console.WriteLine("\n        ┏━━━━━┓");
            Console.WriteLine("        ┃●   ●┃");
            Console.WriteLine("        ┃     ┃");
            Console.WriteLine("        ┃●   ●┃");
            Console.WriteLine("        ┗━━━━━┛");
            break;
        case 5:
            Console.WriteLine("\n        ┏━━━━━┓");
            Console.WriteLine("        ┃●   ●┃");
            Console.WriteLine("        ┃  ●  ┃");
            Console.WriteLine("        ┃●   ●┃");
            Console.WriteLine("        ┗━━━━━┛");
            break;
        case 6:
            Console.WriteLine("\n        ┏━━━━━┓");
            Console.WriteLine("        ┃●   ●┃");
            Console.WriteLine("        ┃●   ●┃");
            Console.WriteLine("        ┃●   ●┃");
            Console.WriteLine("        ┗━━━━━┛");
            break;
    }
}
