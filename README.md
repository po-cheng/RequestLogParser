## RequestLogParser

This is a tool that parses web request logs and displays simple analysis about them

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

#### Development

```sh
dotnet run --project App.RequestLogParser
```

#### Production

```
<path_to_production_build_output>/App.RequestLogParser
```
