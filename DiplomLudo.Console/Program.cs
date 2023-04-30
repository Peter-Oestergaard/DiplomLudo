using System.Text;
using System.Text.RegularExpressions;

Console.OutputEncoding = Encoding.UTF8;

string r = "\u001b[31m";
string g = "\u001b[32m";
string y = "\u001b[33m";
string b = "\u001b[34m";
string w = "\u001b[37m";

string gameBoard =
    "\n" +
    $"{w}                  ┏━━┳━━┳━━┓\n" +
    $"{w}                  ┃10┃11┃12┃\n" +
    $"{w}                  ┣━━{g}╋━━╋━━┫  ┏━━┳━━┓ G\n" +
    $"{w}                  ┃09{g}┃g0┃13┃<━┃gw┃gx┃ r\n" +
    $"{r}   Red            {w}┣━━{g}╋━━╋━━┫  ┣━━╋━━┫ e\n" +
    $"{r}   ┏━━┳━━┓        {w}┃08{g}┃g1┃{w}14┃  {g}┃gy┃gz┃ e\n" +
    $"{r}   ┃rw┃rx┃        {w}┣━━{g}╋━━╋{w}━━┫  {g}┗━━┻━━┛ n\n" +
    $"{r}   ┣━━╋━━┫        {w}┃07{g}┃g2┃{w}15┃\n" +
    $"{r}   ┃ry┃rz┃        {w}┣━━{g}╋━━╋{w}━━┫\n" +
    $"{r}   ┗━━┻━━┛        {w}┃06{g}┃g3┃{w}16┃\n" +
    $"{r}      ┃           {w}┣━━{g}╋━━╋{w}━━┫\n" +
    $"{r}      V           {w}┃05{g}┃g4┃{w}17┃\n" +
    $"{w}┏━━{r}┳━━┳{w}━━┳━━┳━━┳━━╋━━{g}╋━━╋{w}━━╋━━┳━━┳━━┳━━┳━━┳━━┓\n" +
    $"{w}┃51{r}┃00┃{w}01┃02┃03┃04┃██{g}┃g5┃{w}██┃18┃19┃20┃21┃22┃23┃\n" +
    $"{w}┣━━{r}╋━━╋━━╋━━╋━━╋━━╋━━{w}╋{g}━━{w}╋{y}━━╋━━╋━━╋━━╋━━╋━━╋{w}━━┫\n" +
    $"{w}┃50{r}┃r0┃r1┃r2┃r3┃r4┃r5┃{w}██{y}┃y5┃y4┃y3┃y2┃y1┃y0┃{w}24┃\n" +
    $"{w}┣━━{r}╋━━╋━━╋━━╋━━╋━━╋━━{w}╋━━╋{y}━━╋━━╋━━╋━━╋━━╋━━╋{w}━━┫\n" +
    $"{w}┃49┃48┃47┃46┃45┃44┃██┃b5┃██┃30┃29┃28┃27{y}┃26┃{w}25┃\n" +
    $"{w}┗━━┻━━┻━━┻━━┻━━┻━━╋━━╋{b}━━{w}╋━━╋━━┻━━┻━━┻━━{y}┻━━┻{w}━━┛\n" +
    $"{w}                  {w}┃43{b}┃b4┃{w}31┃        {y}    ^\n" +
    $"{w}                  {w}┣━━{b}╋━━╋{w}━━┫        {y}    ┃\n" +
    $"{w}                  {w}┃42{b}┃b3┃{w}32┃        {y}┏━━┳━━┓\n" +
    $"{w}                  {w}┣━━{b}╋━━╋{w}━━┫        {y}┃yw┃yx┃\n" +
    $"{w}                  {w}┃41{b}┃b2┃{w}33┃        {y}┣━━╋━━┫\n" +
    $"{b}       B ┏━━┳━━┓  {w}┣━━{b}╋━━╋{w}━━┫        {y}┃yy┃yz┃\n" +
    $"{b}       l ┃bw┃bx┃  {w}┃40{b}┃b1┃{w}34┃        {y}┗━━┻━━┛\n" +
    $"{b}       u ┣━━╋━━┫  ┣━━╋━━╋{w}━━┫        {y}Yellow\n" +
    $"{b}       e ┃by┃bz┃━>┃39┃b0┃{w}35┃\n" +
    $"{b}         ┗━━┻━━┛  ┣━━╋━━╋{w}━━┫\n" +
    $"{w}                  {w}┃38┃37┃36┃\n" +
    $"{w}                  {w}┗━━┻━━┻━━┛";

bool running = true;
var sb = new StringBuilder(gameBoard);

Regex tileContentIds = new(@"[rgybwxyz0-9]{2}");

string output = tileContentIds.Replace(sb.ToString(), "XX");

do
{
    Console.WriteLine(output);
    var input = Console.ReadKey(false);

    if (input.Key == ConsoleKey.Escape
        || input.Key == ConsoleKey.Q)
    {
        running = false;
        continue;
    }
} while (running);

Environment.Exit(0);
