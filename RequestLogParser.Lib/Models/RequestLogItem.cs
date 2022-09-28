namespace ReqLogParser.Lib.Models;

public record struct RequestLogItem(string Host, DateTime time, string method, string path, int status, int size);
