using System.Text.Json;
using System.CommandLine;
using RequestLogParser.Lib.Utilities;
using RequestLogParser.Lib.Models;

public class Program
{
  public static async Task<int> Main(string[] args)
  {
    var inputOption = new Option<string>(
      name: "--input",
      description: "Path of the log file to parse"
    )
    {
      IsRequired = true
    };

    var outputOption = new Option<string>(
      name: "--output",
      description: "Path to place the output file",
      getDefaultValue: () => "./output.txt"
    );

    var startDateOption = new Option<DateTime?>(
      name: "--startDate",
      description: "The start date and time to filter the logs by. [format: YYYY-mm-dd HH:MM:SS]"
    );

    var endDateOption = new Option<DateTime?>(
      name: "--endDate",
      description: "The end date and time to filter the logs by"
    );

    var command = new RootCommand();
    command.Add(inputOption);
    command.Add(outputOption);
    command.Add(startDateOption);
    command.Add(endDateOption);

    command.SetHandler(async (inputPath, outputPath, startDate, endDate) =>
    {
      await ProcessFile(inputPath, outputPath, startDate, endDate);
    },
    inputOption, outputOption, startDateOption, endDateOption);

    return await command.InvokeAsync(args);
  }

  private static async Task ProcessFile(string inputPath, string outputPath, DateTime? startDate, DateTime? endDate)
  {
    if (File.Exists(inputPath))
    {
      var parser = new LogFileParser(inputPath, quotedFields: true);
      var logItems = parser.ReadLogItems()
        .Where(x => startDate.HasValue && x.time >= startDate.Value || !startDate.HasValue)
        .Where(x => endDate.HasValue && x.time <= endDate.Value || !endDate.HasValue);

      Func<IGrouping<string, RequestLogItem>, (string Key, int Value)> countSelector = s => (Key: s.Key, Value: s.Count());
      var chartData = new List<Series>
      {
        new Series(
          Name: "Requests per Host",
          Data: logItems.GroupBy(x => x.Host)
            .Select(countSelector)
            .OrderByDescending(x => x.Value)
        ),
        new Series(
          Name: "Successful GET Requests",
          Data: logItems.Where(x => x.method == "GET" && x.status == 200)
            .GroupBy(x => x.path)
            .Select(countSelector)
            .OrderByDescending(x => x.Value)
        )
      };
      var renderer = new ResultRenderer(chartData);
      var result = renderer.RenderAsText();

      await File.WriteAllTextAsync(outputPath, result);
    }
    else
    {
      Console.WriteLine($"Log file {inputPath} does not exist. Please check your input path.");
    }
  }
}
