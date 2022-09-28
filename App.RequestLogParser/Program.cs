using System.Collections.Generic;
using System.Text.Json;
using RequestLogParser.Lib.Utilities;

var inputPath = Path.GetFullPath("./Data/test.txt");
var parser = new LogFileParser(inputPath, quotedFields: true);
var logItems = parser.ReadLogItems();


Console.WriteLine(JsonSerializer.Serialize(logItems));
