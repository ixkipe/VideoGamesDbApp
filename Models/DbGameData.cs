namespace RecMeAnOldieWebAPI.Models
{
  public class DbGameDataAccess
  {
    NpgsqlConnection _connection;

    public DbGameDataAccess(NpgsqlConnection connection)
    {
      this._connection = connection;
    }

    public async Task<VideoGame> GetGameById(int id)
    {
      List<string> screenshots = new();
      List<string> genres = new();
      List<string> modes = new();

      var dbScreenshotsData = await _connection.QueryAsync<string>("spGames_GetScreenshots", new { gameId = id }, commandType: System.Data.CommandType.Text);
      foreach (var screenshot in dbScreenshotsData) screenshots.Add(screenshot);

      var dbGenresData = await _connection.QueryAsync<string>("spGames_GetGenres", new { gameId = id }, commandType: System.Data.CommandType.Text);
      foreach (var genre in dbGenresData) genres.Add(genre);

      var dbModesData = await _connection.QueryAsync<string>("spGames_GetModes", new { gameId = id }, commandType: System.Data.CommandType.Text);
      foreach (var mode in dbModesData) modes.Add(mode);

      var dbGameData = await _connection.QueryFirstAsync<VideoGamePartial>("spGames_GetGame", new { gameId = id }, commandType: System.Data.CommandType.Text);
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
