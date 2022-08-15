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
      var dbScreenshotsData = await _connection.QueryAsync<string>(GameDbStoredProcedures.GetScreenshots, new { gameid = id });

      var dbGenresData = await _connection.QueryAsync<string>(GameDbStoredProcedures.GetGenres, new { gameid = id });

      var dbModesData = await _connection.QueryAsync<string>(GameDbStoredProcedures.GetModes, new { gameid = id });

      var dbGameData = await _connection.QueryFirstAsync<VideoGamePartial>(GameDbStoredProcedures.GetGame, new { gameid = id });
      
      return new VideoGame()
      {
        Id = id,
        Title = dbGameData.Title,
        Developer = dbGameData.Developer,
        Platform_Name = dbGameData.Platform_Name,
        Cover_Url = dbGameData.Cover_Url,
        ScreenshotUrlList = dbScreenshotsData.ToList(),
        GenreList = dbGenresData.ToList(),
        ModeList = dbModesData.ToList()
      };
    }
  }
}
