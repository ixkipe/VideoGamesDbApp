public static class GameDbStoredProcedures{
  private const string GetGamesTable = "select \"VideoGames\".\"Games\".id, \"VideoGames\".\"Games\".title, \"VideoGames\".\"Games\".developer, \"VideoGames\".\"Platforms\".platform_name, \"VideoGames\".\"Games\".cover_url from \"VideoGames\".\"Games\" inner join \"VideoGames\".\"Platforms\" on \"VideoGames\".\"Games\".platform_id = \"VideoGames\".\"Platforms\".id";
  private const string GetGenresTable = "select \"VideoGames\".\"GameToGenreReference\".game_id, \"VideoGames\".\"Genres\".genre_name from \"VideoGames\".\"GameToGenreReference\" inner join \"VideoGames\".\"Genres\" on \"VideoGames\".\"GameToGenreReference\".genre_id = \"VideoGames\".\"Genres\".id";
  private const string GetModesTable = "select \"VideoGames\".\"GameTo2PModeReference\".game_id, \"VideoGames\".\"TwoPlayerModes\".mode from \"VideoGames\".\"GameTo2PModeReference\" inner join \"VideoGames\".\"TwoPlayerModes\" on \"VideoGames\".\"GameTo2PModeReference\".mode_id = \"VideoGames\".\"TwoPlayerModes\".id";
  public const string AddGame = "insert into \"VideoGames\".\"Games\" (id, title, developer, platform_id, cover_url) values (((select count(*) from \"VideoGames\".\"Games\")+1), @title, @developer, @platformid, @coverurl)";
  public const string AddScreenshot = "insert into \"VideoGames\".\"Screenshots\" values (@gameid, @screenshoturl)";
  public const string AddGenre = "insert into \"VideoGames\".\"GameToGenreReference\" values (@gameid, @genreid)";
  public const string AddMode = "insert into \"VideoGames\".\"GameTo2PModeReference\" values (@gameid, @modeid)";
  public const string GetGame = $"select * from ({GetGamesTable}) as GamesTable where id = @gameid";
  public const string GetScreenshots = $"select screenshot_url from \"VideoGames\".\"Screenshots\" where game_id = @gameid";
  public const string GetGenres = $"select genre_name from ({GetGenresTable}) as Genres where game_id = @gameid";
  public const string GetModes = $"select \"mode\" from ({GetModesTable}) as Modes where game_id = @gameid";
}