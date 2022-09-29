using System;
namespace RequestLogParser.Lib.Test;

using RequestLogParser.Lib.Utilities;
using RequestLogParser.Lib.Models;

public class LogParserTest
{
  [Theory]
  [InlineData("Data/log-item-sample.txt", "141.243.1.172", "2022-08-29T23:53:25-04:00", "GET", "/Software.html", 200, 1497)]
  [InlineData("Data/404-item-sample.txt", "eso2.orl.mmc.com", "2022-08-30T15:00:35-04:00", "GET", "/docs/convert.html", 404, 0)]
  [InlineData("Data/no-protocol-sample.txt", "141.243.1.172", "2022-08-29T23:53:25-04:00", "GET", "/Software.html", 200, 1497)]
  public void ResultModelShouldMatch(string filePath, string host, string timrStr, string method, string path, int status, int size)
  {
    var parser = new LogFileParser(filePath, quotedFields: true);
    var results = parser.ReadLogItems().Single();

    var expected = new RequestLogItem(host, DateTime.Parse(timrStr).ToUniversalTime(), method, path, status, size);
    Assert.Equal(expected, results);
  }
}
