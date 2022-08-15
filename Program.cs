using RecMeAnOldieWebAPI.Models;

string connectionString = File.ReadAllText("C:/Users/user/Documents/postgres_login.txt");
var menu = new DbGameAdminMenu(new(connectionString));
await menu.AddGames();

// var game = await new DbGameDataAccess(new(connectionString)).GetGameById(180);
// System.Console.WriteLine($"Title: {game.Title}");
// System.Console.WriteLine($"Developed by {game.Developer}");
// System.Console.WriteLine($"Platform: {game.Platform_Name}");
// System.Console.WriteLine($"Cover URL: {game.Cover_Url}");
// System.Console.WriteLine("Screenshots:");
// foreach (var screenshot in game.ScreenshotUrlList) System.Console.WriteLine(screenshot);
// System.Console.WriteLine("Genres: ");
// foreach (var genre in game.GenreList) System.Console.Write(genre + " ");
// System.Console.WriteLine("\n2 player modes:");
// foreach (var mode in game.ModeList) System.Console.Write(mode + " ");