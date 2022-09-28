namespace RequestLogParser.Lib.Test;

using RequestLogParser.Lib.Utilities;
using RequestLogParser.Lib.Models;

public class ResultRenderTest
{
  [Theory]
  [MemberData(nameof(generateSample))]
  public void ResultModelShouldMatch(IEnumerable<Series> data, string expected)
  {
    var renderer = new ResultRenderer(data);
    var result = renderer.RenderAsText();
    Assert.Equal(expected, result);
  }

  private static IEnumerable<Object[]> generateSample()
  {
    yield return new object[]
    {
      new Series[]
      {
        new Series(
          Name: "Section 1",
          Data: new (string, int)[]
          {
            ("AAAAA", 3),
            ("BBBBB", 1)
          }
        ),
        new Series(
          Name: "Section 2",
          Data: new (string, int)[]
          {
            ("CCC", 6),
            ("DDD", 999)
          }
        )
      },
      $"Section 1\nAAAAA 3\nBBBBB 1\n\nSection 2\nCCC 6\nDDD 999"
    };
  }
}
