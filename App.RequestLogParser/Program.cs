using System.Collections.Generic;
using System.Text.Json;
using RequestLogParser.Lib.Utilities;

var inputPath = "./App.RequestLogParser/test.txt";
var parser = new LogFileParser(Path.GetFullPath(inputPath), quotedFields: true);
var logItems = parser.ReadLogItems();


Console.WriteLine(JsonSerializer.Serialize(logItems));
