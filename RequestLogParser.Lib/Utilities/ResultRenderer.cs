namespace RequestLogParser.Lib.Utilities;

using System.Text.Json;
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
      var seriesData = c.Data.Select(s => $"{s.Key} {s.Value}");
      p = $"{p}{c.Name}\n{String.Join('\n', seriesData)}\n\n";
      return p;
    });
  }

  public string RenderAsJson()
  {
    return JsonSerializer.Serialize(_data);
  }
}
