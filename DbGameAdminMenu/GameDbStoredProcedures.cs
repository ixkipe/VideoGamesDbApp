public static class GameDbStoredProcedures{
  public const string AddGame = "insert into \"VideoGames\".\"Games\" (id, title, developer, platform_id, cover_url) values (((select count(*) from \"VideoGames\".\"Games\")+1), @title, @developer, @platformid, @coverurl)";
  public const string AddScreenshot = "insert into \"VideoGames\".\"Screenshots\" values (@gameid, @screenshoturl)";
  public const string AddGenre = "insert into \"VideoGames\".\"GameToGenreReference\" values (@gameid, @genreid)";
  public const string AddMode = "insert into \"VideoGames\".\"GameTo2PModeReference\" values (@gameid, @modeid)";
}