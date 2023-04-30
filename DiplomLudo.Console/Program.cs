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
    "{w}                  ┃09{g}┃g0┃13{g}┃◀━┃gw┃gx┃ r\n" +
    "{r}   Red            {w}┣━━{g}╋━━╋━━┫  ┣━━╋━━┫ e\n" +
    "{r}   ┏━━┳━━┓        {w}┃08{g}┃g1┃{w}14┃  {g}┃gy┃gz┃ e\n" +
    "{r}   ┃rw┃rx┃        {w}┣━━{g}╋━━╋{w}━━┫  {g}┗━━┻━━┛ n\n" +
    "{r}   ┣━━╋━━┫        {w}┃07{g}┃g2┃{w}15┃\n" +
    "{r}   ┃ry┃rz┃        {w}┣━━{g}╋━━╋{w}━━┫\n" +
    "{r}   ┗━━┻━━┛        {w}┃06{g}┃g3┃{w}16┃\n" +
    "{r}     ┃            {w}┣━━{g}╋━━╋{w}━━┫\n" +
    "{r}     ▼            {w}┃05{g}┃g4┃{w}17┃\n" +
    "{w}┏━━{r}┳━━┳{w}━━┳━━┳━━┳━━╋━━{g}╋━━╋{w}━━╋━━┳━━┳━━┳━━┳━━┳━━┓\n" +
    "{w}┃51{r}┃00{r}┃{w}01┃02┃03┃04┃██{g}┃g5┃{w}██┃18┃19┃20┃21┃22┃23┃\n" +
    "{w}┣━━{r}╋━━╋━━╋━━╋━━╋━━╋━━{w}╋{g}━━{w}╋{y}━━╋━━╋━━╋━━╋━━╋━━╋{w}━━┫\n" +
    "{w}┃50{r}┃r0┃r1┃r2┃r3┃r4┃r5┃{w}██{y}┃y5┃y4┃y3┃y2┃y1┃y0┃{w}24┃\n" +
    "{w}┣━━{r}╋━━╋━━╋━━╋━━╋━━╋━━{w}╋━━╋{y}━━╋━━╋━━╋━━╋━━╋━━╋{w}━━┫\n" +
    "{w}┃49┃48┃47┃46┃45┃44┃██┃b5┃██┃30┃29┃28┃27{y}┃26{y}┃{w}25┃\n" +
    "{w}┗━━┻━━┻━━┻━━┻━━┻━━╋━━╋{b}━━{w}╋━━╋━━┻━━┻━━┻━━{y}┻━━┻{w}━━┛\n" +
    "{w}                  {w}┃43{b}┃b4┃{w}31┃        {y}    ▲\n" +
    "{w}                  {w}┣━━{b}╋━━╋{w}━━┫        {y}    ┃\n" +
    "{w}                  {w}┃42{b}┃b3┃{w}32┃        {y}┏━━┳━━┓\n" +
    "{w}                  {w}┣━━{b}╋━━╋{w}━━┫        {y}┃yw┃yx┃\n" +
    "{w}                  {w}┃41{b}┃b2┃{w}33┃        {y}┣━━╋━━┫\n" +
    "{b}       B ┏━━┳━━┓  {w}┣━━{b}╋━━╋{w}━━┫        {y}┃yy┃yz┃\n" +
    "{b}       l ┃bw┃bx┃  {w}┃40{b}┃b1┃{w}34┃        {y}┗━━┻━━┛\n" +
    "{b}       u ┣━━╋━━┫  ┣━━╋━━╋{w}━━┫        {y}Yellow\n" +
    "{b}       e ┃by┃bz┃━▶┃39{b}┃b0┃{w}35┃\n" +
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
game.StartingPlayer(players[(Color) new Random().Next(0, players.Count)]);
// game.StartingPlayer(players[Color.Red]);
do
{
    int dieRoll = game.RollDie();
    var moves = game.PiecesWithLegalMoves();

    // Reset gameboard
    string gameBoard = gameBoardTemplate;

    // Update pieces
    gameBoard = UpdateBoard(gameBoard);

    // Update possible moves
    gameBoard = UpdatePossibleMoves(gameBoard, moves, dieRoll);

    // Clear control codes
    Regex tileContentIds = new(@"[rgybwxyz0-9]{2}");
    gameBoard = tileContentIds.Replace(gameBoard, "  ");

    // Insert colors
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
    switch (game.CurrentPlayer!.Color)
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
    Console.WriteLine($" Current player: {colorCode}{Enum.GetName(game.CurrentPlayer!.Color)}{w}\n");
    RenderDie(dieRoll);

    // Dictionary<int, Piece> pieceChoice = new Dictionary<int, Piece>();
    // Dictionary<int, Tile> moveToRender = new Dictionary<int, Tile>();

    foreach (Piece piece in moves)
    {
        Console.WriteLine("    You can make a move");
    }

    // Get player input
    var input = Console.ReadKey(false);

    if (input.Key == ConsoleKey.Escape
        || input.Key == ConsoleKey.Q)
    {
        running = false;
        continue;
    }
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
            Console.WriteLine("        ┏━━━━━┓");
            Console.WriteLine("        ┃●    ┃");
            Console.WriteLine("        ┃     ┃");
            Console.WriteLine("        ┃    ●┃");
            Console.WriteLine("        ┗━━━━━┛");
            break;
        case 3:
            Console.WriteLine("        ┏━━━━━┓");
            Console.WriteLine("        ┃●    ┃");
            Console.WriteLine("        ┃  ●  ┃");
            Console.WriteLine("        ┃    ●┃");
            Console.WriteLine("        ┗━━━━━┛");
            break;
        case 4:
            Console.WriteLine("        ┏━━━━━┓");
            Console.WriteLine("        ┃●   ●┃");
            Console.WriteLine("        ┃     ┃");
            Console.WriteLine("        ┃●   ●┃");
            Console.WriteLine("        ┗━━━━━┛");
            break;
        case 5:
            Console.WriteLine("        ┏━━━━━┓");
            Console.WriteLine("        ┃●   ●┃");
            Console.WriteLine("        ┃  ●  ┃");
            Console.WriteLine("        ┃●   ●┃");
            Console.WriteLine("        ┗━━━━━┛");
            break;
        case 6:
            Console.WriteLine("        ┏━━━━━┓");
            Console.WriteLine("        ┃●   ●┃");
            Console.WriteLine("        ┃●   ●┃");
            Console.WriteLine("        ┃●   ●┃");
            Console.WriteLine("        ┗━━━━━┛");
            break;
    }
}
