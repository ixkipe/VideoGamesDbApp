namespace RecMeAnOldieWebAPI.Models
{
  public class DbGameDataAccess
  {
    SqlConnection _connection;

    public DbGameDataAccess(SqlConnection connection)
    {
      this._connection = connection;
    }

    public async Task<VideoGame> GetGameById(int id)
    {
      List<string> screenshots = new();
      List<string> genres = new();
      List<string> modes = new();

      var dbScreenshotsData = await _connection.QueryAsync<string>("VideoGames.spGames_GetScreenshotsByID", new { gameId = id }, commandType: System.Data.CommandType.StoredProcedure);
      foreach (var screenshot in dbScreenshotsData) screenshots.Add(screenshot);

      var dbGenresData = await _connection.QueryAsync<string>("VideoGames.spGames_GetGenresByID", new { gameId = id }, commandType: System.Data.CommandType.StoredProcedure);
      foreach (var genre in dbGenresData) genres.Add(genre);

      var dbModesData = await _connection.QueryAsync<string>("VideoGames.spGames_GetModesByID", new { gameId = id }, commandType: System.Data.CommandType.StoredProcedure);
      foreach (var mode in dbModesData) modes.Add(mode);

      var dbGameData = await _connection.QueryFirstAsync<VideoGamePartial>("VideoGames.spGames_GetGame", new { gameId = id }, commandType: System.Data.CommandType.StoredProcedure);

      return new VideoGame()
      {
        Id = id,
        Title = dbGameData.Title,
        Developer = dbGameData.Developer,
        Platform_Name = dbGameData.Platform_Name,
        Cover_Url = dbGameData.Cover_Url,
        ScreenshotUrlList = screenshots,
        GenreList = genres,
        ModeList = modes
      };
    }
  }
}
