# peaccounting-net
API wrapper for p:e accounting built in .NET Standard.

https://www.accounting.pe/vara-tjanster/integrationer

## Models
The models are auto generated using [xsd.exe](https://docs.microsoft.com/en-us/dotnet/standard/serialization/xml-schema-definition-tool-xsd-exe) from the [XSD](https://my.accounting.pe/api/v1/xsd).

To generate new classes, download a new version of the XSD, place it in /models and run the following command from [Developer Command Prompt for Visual Studio](https://docs.microsoft.com/en-us/dotnet/framework/tools/developer-command-prompt-for-vs).
```
xsd Models.xsd /c /n:Orneholm.PEAccountingNet.Models
```