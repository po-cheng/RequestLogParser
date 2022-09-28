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
    var seriesResults = _data.Select(s =>
    {
      var seriesData = s.Data.Select(d => $"{d.Key} {d.Value}");
      return $"{s.Name}\n{String.Join('\n', seriesData)}";
    });
    return String.Join("\n\n", seriesResults);
  }

  public string RenderAsJson()
  {
    return JsonSerializer.Serialize(_data);
  }
}
