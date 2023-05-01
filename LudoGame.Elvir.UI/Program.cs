// See https://aka.ms/new-console-template for more information

using LudoGame.Elvir.UI;
using LudoGameElvir;

var players = new List<IPlayer>(); // Create an empty list of players initially
var game = new Game(players);
var ludoConsole = new LudoService(game);
ludoConsole.Start();