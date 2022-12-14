namespace RequestLogParser.Lib.Utilities;

using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;
using RequestLogParser.Lib.Models;

public class LogFileParser
{
  private string _filePath;
  private string _delimiter;
  private bool _quotedFields;

  public LogFileParser(string filePath, string delimiter = " ", bool quotedFields = false)
  {
    _filePath = filePath;
    _delimiter = delimiter;
    _quotedFields = quotedFields;
  }

  public IEnumerable<RequestLogItem> ReadLogItems()
  {
    using var parser = new TextFieldParser(_filePath);
    parser.SetDelimiters(new string[] { _delimiter });
    parser.HasFieldsEnclosedInQuotes = _quotedFields;

    while (!parser.EndOfData)
    {
      var fields = parser.ReadFields();

      if (fields != null)
      {
        var host = fields[0];

        // deal with time string
        var ts = @$"2022-08-{fields[1]} -04:00";
        var time = DateTime.ParseExact(ts, "yyyy-MM-[dd:HH:mm:ss] zzz", CultureInfo.CurrentCulture).ToUniversalTime();

        // separate method, path, and protocol
        var pathRegex = new Regex(@"(?<method>[A-Z]+)\s(?<path>\S+)");
        var match = pathRegex.Match(fields[2]);
        var method = match.Groups["method"]?.Value ?? "";
        var reqPath = match.Groups["path"]?.Value ?? "";

        Int32.TryParse(fields[3], out var status);
        Int32.TryParse(fields[4], out var size);
        var logItem = new RequestLogItem(host, time, method, reqPath, status, size);

        yield return logItem;
      }
    }
  }
}
