namespace RequestLogParser.Lib.Models;

public record struct Series(string Name, IEnumerable<(string Key, int Value)> data);
