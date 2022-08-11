using RecMeAnOldieWebAPI.Models;
using System.Collections;

class DbGameAdminMenu {
  NpgsqlConnection _connection;

  public DbGameAdminMenu(NpgsqlConnection connection)
  {
    this._connection = connection;
  }

  public async Task AddGames(){
    bool quit = false;
    while (!quit)
    {
      await AddGame();
      System.Console.WriteLine("Add another game? (Y/N)");
      var cki = Console.ReadKey();

      switch (cki.KeyChar.ToString().ToLower()){
        case "y":
        case "н":
          continue;
        default:
          quit = true;
          break;
      }
    }
  }

  private async Task AddGame() {
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

      System.Console.Write("Platform (1. NES 2. Genesis 3. SNES 4. PC): ");
      game.PlatformID = Int32.Parse(Console.ReadLine());
      System.Console.WriteLine();

      System.Console.WriteLine("Cover URL: ");
      game.CoverUrl = Console.ReadLine();
      System.Console.WriteLine();

      System.Console.WriteLine("Check the values again:");
      System.Console.WriteLine($"Title: {game.Title}\nDeveloper: {game.Developer}\nPlatformID: {game.PlatformID}\nCover URL: {game.CoverUrl}");
      System.Console.WriteLine("Is everything correct? (Y/N)");
      var cki = Console.ReadKey();
      switch (cki.KeyChar.ToString().ToLower()){
        case "y":
        case "н":
          ba[0] = false;
          break;
        default: break;
      }
    }
    int id = 0;
    
    Task.Run(async () => {
      await _connection.QueryAsync(
        GameDbStoredProcedures.AddGame, 
        new {title = game.Title, developer = game.Developer, platformid = game.PlatformID, coverurl = game.CoverUrl});

      id = await _connection.QueryFirstAsync<int>("select count(*) from \"VideoGames\".\"Games\"");
    });


    while (ba[1])
    {
      System.Console.Write("\nAdd screenshot URL: ");
      game.ScreenshotUrlList.Add(Console.ReadLine());
      System.Console.WriteLine();
      System.Console.WriteLine("Add another one? (Y/N)");
      var cki = Console.ReadKey();
      switch (cki.KeyChar.ToString().ToLower()){
        case "y":
        case "н":
          continue;
        default: break;
      }

      System.Console.WriteLine("Is everything correct?");
      foreach (var screenshot in game.ScreenshotUrlList)
        System.Console.WriteLine(screenshot);

      cki = Console.ReadKey();
      switch (cki.KeyChar.ToString().ToLower()){
        case "y":
        case "н":
          ba[1] = false;
          break;
        default:
          game.ScreenshotUrlList.Clear();
          break;
      }
    }

    foreach (var screenshot in game.ScreenshotUrlList)
      await _connection.QueryAsync(
        GameDbStoredProcedures.AddScreenshot, 
        new {gameid = id, screenshoturl = screenshot});
    
    var genres = await GetGenres();
    System.Console.WriteLine("Genres reference:");
    for (int i = 0; i < genres.Count; i++) System.Console.WriteLine($"ID: {i+1} Genre name: {genres[i]}");

    while (ba[2])
    {
      System.Console.Write("\nEnter genre ID: ");
      game.GenreList.Add(Int32.Parse(Console.ReadLine()));

      System.Console.WriteLine("Add another one? (Y/N)");
      var cki = Console.ReadKey();
      switch (cki.KeyChar.ToString().ToLower()){
        case "y":
        case "н":
          continue;
        default: break;
      }

      System.Console.WriteLine("Is everything correct?");
      foreach (var genre in game.GenreList)
        System.Console.WriteLine(genre);

      cki = Console.ReadKey();
      switch (cki.KeyChar.ToString().ToLower()){
        case "y":
        case "н":
          ba[2] = false;
          break;
        default: 
          game.GenreList.Clear();
          break;
      }
    }

    foreach (var genre in game.GenreList)
      await _connection.QueryAsync(
        GameDbStoredProcedures.AddGenre, 
        new {gameid = id, genreid =  genre});

    var modes = await GetModes();
    System.Console.WriteLine("Modes reference:");
    for (int i = 0; i < modes.Count; i++) System.Console.WriteLine($"ID: {i+1} Mode: {modes[i]}");

    while (ba[3])
    {
      System.Console.Write("Enter mode ID: ");
      game.ModeList.Add(int.Parse(Console.ReadLine()));

      System.Console.WriteLine("Add another one? (Y/N)");
      var cki = Console.ReadKey();
      switch (cki.KeyChar.ToString().ToLower()){
        case "y":
        case "н":
          continue;
        default: break;
      }

      System.Console.WriteLine("Is everything correct?");
      foreach (var mode in game.ModeList)
        System.Console.WriteLine(mode);

      cki = Console.ReadKey();
      switch (cki.KeyChar.ToString().ToLower()){
        case "y":
        case "н":
          ba[3] = false;
          break;
        default: 
          game.ModeList.Clear();
          break;
      }

      foreach (var mode in game.ModeList)
        await _connection.QueryAsync(
          GameDbStoredProcedures.AddMode, 
          new{gameid = id, modeid = mode});

    }
  }

  private async Task<List<string>> GetGenres(){
    var result = await _connection.QueryAsync<string>("select genre_name from \"VideoGames\".\"Genres\"");
    return result.ToList();
  }

  private async Task<List<string>> GetModes(){
    var result = await _connection.QueryAsync<string>("select mode from \"VideoGames\".\"TwoPlayerModes\"");
    return result.ToList();
  }
}