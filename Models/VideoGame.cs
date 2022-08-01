namespace RecMeAnOldieWebAPI.Models
{
  public class VideoGame
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Developer { get; set; }
    public string Platform_Name { get; set; }
    public string Cover_Url { get; set; }
    public List<string> ScreenshotUrlList { get; set; }
    public List<string> GenreList { get; set; }
    public List<string> ModeList { get; set; }
  }
  
  public class VideoGamePartial
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Developer { get; set; }
    public string Platform_Name { get; set; }
    public string Cover_Url { get; set; }
  }

  public class VideoGameAddDataModel{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Developer { get; set; }
    public int PlatformID { get; set; }
    public string CoverUrl { get; set; }
    public List<string> ScreenshotUrlList { get; set; } = new();
    public List<int> GenreList { get; set; } = new();
    public List<int> ModeList { get; set; } = new();
  }
}
