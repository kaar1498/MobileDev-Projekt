namespace MobileDev_Projekt.Entities
{
  public class Large
  {
    public string ext { get; set; }
    public string url { get; set; }
    public string hash { get; set; }
    public string mime { get; set; }
    public string name { get; set; }
    public object path { get; set; }
    public double size { get; set; }
    public int width { get; set; }
    public int height { get; set; }
  }

  public class Small
  {
    public string ext { get; set; }
    public string url { get; set; }
    public string hash { get; set; }
    public string mime { get; set; }
    public string name { get; set; }
    public object path { get; set; }
    public double size { get; set; }
    public int width { get; set; }
    public int height { get; set; }
  }

  public class Medium
  {
    public string ext { get; set; }
    public string url { get; set; }
    public string hash { get; set; }
    public string mime { get; set; }
    public string name { get; set; }
    public object path { get; set; }
    public double size { get; set; }
    public int width { get; set; }
    public int height { get; set; }
  }

  public class Thumbnail
  {
    public string ext { get; set; }
    public string url { get; set; }
    public string hash { get; set; }
    public string mime { get; set; }
    public string name { get; set; }
    public object path { get; set; }
    public double size { get; set; }
    public int width { get; set; }
    public int height { get; set; }
  }

  public class Formats
  {
    public Large large { get; set; }
    public Small small { get; set; }
    public Medium medium { get; set; }
    public Thumbnail thumbnail { get; set; }
  }
}