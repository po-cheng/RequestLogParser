## RequestLogParser

This is a tool that parses web request logs and displays simple analysis about them

### Tests

```sh
dotnet test
```

### Build

#### Development

```sh
dotnet build .
```

#### Production

- creates a framework independent package

```sh
# linux or wsl
dotnet publish ./App.RequestLogParser --self-contained -r linux-x64 -c Release -o ./<some_output_folder>

# windows 10+
dotnet publish ./App.RequestLogParser --self-contained -r win10-x64 -c Release -o ./<some_output_folder>

# for any other platform please refer to
# https://learn.microsoft.com/en-us/dotnet/core/rid-catalog
# for a full list of RIDs (to change the -r param)
```

### Runing the App

#### Options

- `--input`
  - path to the input log file
  - required
- `--output`
  - path to the output file
  - optional
  - default: "./output.txt"
- `--startDate`
  - start date & time filter for analysis results
  - optional
  - format: yyyy-mm-dd HH:MM:SS
  - value is inclusive
- `--endDate`
  - end date & time filter for analysis results
  - optional
  - format: yyyy-mm-dd HH:MM:SS
  - value is inclusive

#### Development

```sh
# basic usage
dotnet run --project App.RequestLogParser -- --input <path_to_input_file>
```

#### Production

```sh
# linux/wsl basic usage
<path_to_production_build_output>/App.RequestLogParser --input <path_to_input_file>

# windows basic usage
<path_to_production_build_output>\App.RequestLogParser.exe --input <path_to_input_file>
```
