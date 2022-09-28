using System.Text.Json;
using System.CommandLine;
using RequestLogParser.Lib.Utilities;

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

    command.SetHandler((inputPath, outputPath, startDate, endDate) =>
    {
      ReadFile(inputPath, outputPath, startDate, endDate);
    },
    inputOption, outputOption, startDateOption, endDateOption);

    return await command.InvokeAsync(args);
  }

  private static void ReadFile(string inputPath, string outputPath, DateTime? startDate, DateTime? endDate)
  {
    if (File.Exists(inputPath))
    {
      var parser = new LogFileParser(inputPath, quotedFields: true);
      var logItems = parser.ReadLogItems()
        .Where(x => startDate.HasValue && x.time >= startDate.Value || !startDate.HasValue)
        .Where(x => endDate.HasValue && x.time <= endDate.Value || !endDate.HasValue);

      Console.WriteLine(JsonSerializer.Serialize(new { inputPath, outputPath, startDate, endDate }));
      Console.WriteLine(JsonSerializer.Serialize(logItems));
    }
    else
    {
      Console.WriteLine($"Log file {inputPath} does not exist. Please check your input path.");
    }
  }
}
