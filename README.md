# peaccounting-net
API wrapper for [p:e accounting](https://www.accounting.pe/) built in .NET Standard based on the [offical documentation](https://my.accounting.pe/api/v1/doc).

## Gettings started

### Authentication API

The main API is scoped to a single company and restricted to the access rights of a single user given a personal access token.
The `PeaAuthenticationApi` provides a simple way of retrieving those companies and tokens given a _username_ and _password_.

*Example:*
```csharp
var authenticationApi = new PeaAuthenticationApi();
var companies = await authenticationApi.GetAccessibleCompaniesAsync("username", "password");
foreach (var company in companies)
{
   Console.WriteLine($"{company.name} ({company.id}): {company.token}");
}
```

### Data API
```csharp
// List clients
var result = await api.GetClientsAsync();
foreach (var item in result)
{
    Console.WriteLine($"{item.name} ({item.id}): {item.vatnr}");
}

// List client projects
var result = await api.GetClientProjectsAsync();
foreach (var item in result)
{
    Console.WriteLine($"{item.name} ({item.id.id}): {item.comment}");
}
```

### Sample

A console application with samples is available in /samples/Orneholm.PEAccountingNet.ConsoleAppSamle.


## Regenerating models
The models are auto generated using [xsd.exe](https://docs.microsoft.com/en-us/dotnet/standard/serialization/xml-schema-definition-tool-xsd-exe) with the [XSD source from p:e accoutning](https://my.accounting.pe/api/v1/xsd).

To generate new classes, download a new version of the XSD, place it in /models and run the following command from [Developer Command Prompt for Visual Studio](https://docs.microsoft.com/en-us/dotnet/framework/tools/developer-command-prompt-for-vs).
```
xsd Models.xsd /c /n:Orneholm.PEAccountingNet.Models
```

# Todo
This project is in a very early release and will have breaking changes before reaching 1.0.
Pull requests are welcome.

- [x] Initial release
- [ ] Publish to NuGet
- [ ] Unit tests
- [ ] CI/CD pipeline
- [ ] Add missing API:s