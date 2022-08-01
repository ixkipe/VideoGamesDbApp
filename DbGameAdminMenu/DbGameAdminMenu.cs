using RecMeAnOldieWebAPI.Models;
using System.Collections;

class DbGameAdminMenu {
  SqlConnection _connection;

  public DbGameAdminMenu(SqlConnection connection)
  {
    this._connection = connection;
  }

  public async Task AddGame() {
    BitArray ba = new(new bool[]{true, true, true, true});
    VideoGameAddDataModel game = new();

    while (ba[0])
    {
      System.Console.Write("Title: ");
      game.Title = Console.ReadLine();
      System.Console.WriteLine();

      System.Console.Write("Developer: ");
      game.Developer = Console.ReadLine();
      System.Console.WriteLine();

      System.Console.Write("Platform (1. NES 2. Genesis 3. SNES): ");
      game.PlatformID = Int32.Parse(Console.ReadLine());
      System.Console.WriteLine();

      System.Console.WriteLine("Cover URL: ");
      game.CoverUrl = Console.ReadLine();
      System.Console.WriteLine();

      System.Console.WriteLine("Check the values again:");
      System.Console.WriteLine($"Title: {game.Title}\nDeveloper: {game.Developer}\nPlatformID: {game.PlatformID}\nCover URL: {game.CoverUrl}");
      System.Console.WriteLine("Is everything correct? (Y/N)");
      var cki = Console.ReadKey();
      switch (cki.Key){
        case ConsoleKey.Y:
        case ConsoleKey.Enter:
        case ConsoleKey.Spacebar:
          ba[0] = false;
          break;
        default: break;
      }
    }

    await _connection.QueryAsync(
      "VideoGames.spGames_AddGame", 
      new {Title = game.Title, Developer = game.Developer, PlatformID = game.PlatformID, CoverURL = game.CoverUrl}, 
      commandType: System.Data.CommandType.StoredProcedure);

    int id = await _connection.QueryFirstAsync<int>("select count(*) from VideoGames.Games");

    while (ba[1])
    {
      System.Console.Write("\nAdd screenshot URL: ");
      game.ScreenshotUrlList.Add(Console.ReadLine());
      System.Console.WriteLine();
      System.Console.WriteLine("Add another one? (Y/N)");
      var cki = Console.ReadKey();
      switch (cki.Key){
        case ConsoleKey.Y:
        case ConsoleKey.Enter:
        case ConsoleKey.Spacebar:
          continue;
        default: break;
      }

      System.Console.WriteLine("Is everything correct?");
      foreach (var screenshot in game.ScreenshotUrlList)
        System.Console.WriteLine(screenshot);

      cki = Console.ReadKey();
      switch (cki.Key){
        case ConsoleKey.Y:
        case ConsoleKey.Enter:
        case ConsoleKey.Spacebar:
          ba[1] = false;
          break;
        default:
          game.ScreenshotUrlList.Clear();
          break;
      }
    }

    foreach (var screenshot in game.ScreenshotUrlList)
      await _connection.QueryAsync(
        "VideoGames.spGames_AddScreenshot", 
        new {GameID = id, ScreenshotURL = screenshot}, 
        commandType: System.Data.CommandType.StoredProcedure);
    
    var genres = await GetGenres();
    System.Console.WriteLine("Genres reference:");
    for (int i = 0; i < genres.Count; i++) System.Console.WriteLine($"ID: {i+1} Genre name: {genres[i]}");

    while (ba[2])
    {
      System.Console.Write("\nEnter genre ID: ");
      game.GenreList.Add(Int32.Parse(Console.ReadLine()));

      System.Console.WriteLine("Add another one? (Y/N)");
      var cki = Console.ReadKey();
      switch (cki.Key){
        case ConsoleKey.Y:
        case ConsoleKey.Enter:
        case ConsoleKey.Spacebar:
          continue;
        default: break;
      }

      System.Console.WriteLine("Is everything correct?");
      foreach (var genre in game.GenreList)
        System.Console.WriteLine(genre);

      cki = Console.ReadKey();
      switch (cki.Key){
        case ConsoleKey.Y:
        case ConsoleKey.Enter:
        case ConsoleKey.Spacebar:
          ba[2] = false;
          break;
        default: 
          game.GenreList.Clear();
          break;
      }
    }

    foreach (var genre in game.GenreList)
      await _connection.QueryAsync(
        "VideoGames.spGames_AddGenre", 
        new {GameID = id, GenreID =  genre}, 
        commandType: System.Data.CommandType.StoredProcedure);

    var modes = await GetModes();
    System.Console.WriteLine("Modes reference:");
    for (int i = 0; i < modes.Count; i++) System.Console.WriteLine($"ID: {i+1} Mode: {modes[i]}");

    while (ba[3])
    {
      System.Console.Write("Enter mode ID: ");
      game.ModeList.Add(int.Parse(Console.ReadLine()));

      System.Console.WriteLine("Add another one? (Y/N)");
      var cki = Console.ReadKey();
      switch (cki.Key){
        case ConsoleKey.Y:
        case ConsoleKey.Enter:
        case ConsoleKey.Spacebar:
          continue;
        default: break;
      }

      System.Console.WriteLine("Is everything correct?");
      foreach (var mode in game.ModeList)
        System.Console.WriteLine(mode);

      cki = Console.ReadKey();
      switch (cki.Key){
        case ConsoleKey.Y:
        case ConsoleKey.Enter:
        case ConsoleKey.Spacebar:
          ba[3] = false;
          break;
        default: 
          game.ModeList.Clear();
          break;
      }

      foreach (var mode in game.ModeList)
        await _connection.QueryAsync(
          "VideoGames.spGames_Add2PMode", 
          new{GameID = id, ModeID = mode}, 
          commandType: System.Data.CommandType.StoredProcedure);

    }
  }

  private async Task<List<string>> GetGenres(){
    var result = await _connection.QueryAsync<string>("select genre_name from VideoGames.Genres");
    return result.ToList();
  }

  private async Task<List<string>> GetModes(){
    var result = await _connection.QueryAsync<string>("select mode from VideoGames.TwoPlayerModes");
    return result.ToList();
  }
}