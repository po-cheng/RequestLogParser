namespace RequestLogParser.Lib.Utilities;

using RequestLogParser.Lib.Models;

public class ResultRenderer
{
  private IEnumerable<Series> _data;

  public ResultRenderer(IEnumerable<Series> data)
  {
    _data = data;
  }

  public string RenderAsText()
  {
    return _data.Aggregate("", (p, c) =>
    {
      var seriesData = c.data.Select(s => $"{s.Key} {s.Value}");
      p = $"{c.Name}\n{String.Join('\n', seriesData)}\n";
      return p;
    });
  }
}
