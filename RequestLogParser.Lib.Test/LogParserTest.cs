namespace RequestLogParser.Lib.Test;

using RequestLogParser.Lib.Utilities;
using RequestLogParser.Lib.Models;

public class LogParserTest
{
  [Fact]
  public void ResultModelShouldMatch()
  {
    var filePath = Path.GetFullPath("Data/log-item-sample.txt");
    var parser = new LogFileParser(filePath, quotedFields: true);
    var results = parser.ReadLogItems().Single();

    var expected = new RequestLogItem(
        "141.243.1.172",
        DateTime.Parse("2022-08-29T23:53:25-04:00").ToUniversalTime(),
        "GET",
        "/Software.html",
        200,
        1497
    );
    Assert.Equal(expected, results);
  }
}
