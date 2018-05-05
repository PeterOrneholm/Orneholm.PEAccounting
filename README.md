# Orneholm.PEAccountingNet
API wrapper (unofficial) for [p:e accounting](https://www.accounting.pe/) built in C# targeting .NET Standard 2.0. Based on the public [documentation](https://my.accounting.pe/api/v1/doc).

## Installation

The package is published to [NuGet](https://www.nuget.org/packages/Orneholm.PEAccountingNet/).

[![NuGet](https://img.shields.io/nuget/v/Orneholm.PEAccountingNet.svg)](https://www.nuget.org/packages/Orneholm.PEAccountingNet/)

## Usage

### Authentication API

The main API is scoped to a single company and restricted to the access rights of a single user given a personal access token.
The `PeaAuthenticationApi` provides a simple way of retrieving those companies and tokens given a _username_ and _password_.

*Example:*
```csharp
var authenticationApi = new PeaAuthenticationApi();
var companies = await authenticationApi.GetAccessibleCompaniesAsync("username", "password");
foreach (var company in companies)
{
   Console.WriteLine($"{company.Name} ({company.Id}): {company.Token}");
}
```

### Data API

*Example:*
```csharp
var api = new PeaApi(company.id, company.token);

// List clients
var clients = await api.GetClientsAsync();
foreach (var client in clients)
{
    Console.WriteLine($"{client.Name} ({client.Id}): {client.OrgNr}");
}

// List client projects
var clientProjects = await api.GetClientProjectsAsync();
foreach (var clientProject in clientProjects)
{
    Console.WriteLine($"{clientProject.Name} ({clientProject.Id}): {clientProject.Comment}");
}
```

## Getting started

A console application with a few usecases is available in [Samples](samples/Orneholm.PEAccountingNet.ConsoleAppSamle).

## Models

The native models used to map the XML requests and responses are not exposed through the API, but instead beeing mapped against custom models.
This enabled the API to follow the conventions of C# but also makes sure that any breaking change in the models is beeing noticed.

### Regenerating models
The models are auto generated using [xsd.exe](https://docs.microsoft.com/en-us/dotnet/standard/serialization/xml-schema-definition-tool-xsd-exe) with the [XSD source from p:e accoutning](https://my.accounting.pe/api/v1/xsd).

To generate new classes, download a new version of the XSD, place it in /Models/Native and run the following command from [Developer Command Prompt for Visual Studio](https://docs.microsoft.com/en-us/dotnet/framework/tools/developer-command-prompt-for-vs).
```
xsd NativeModels.xsd /c /n:Orneholm.PEAccountingNet.Models.Native
```

## Contributions

Contributions are welcome through Pull Requests. But feel free to file an isuue if you have suggestions of improvement.

### Todo
This project is in a very early release and will have breaking changes before reaching 1.0.
Pull requests are welcome.

- [x] Initial release
- [x] Publish to NuGet
- [ ] Unit tests
- [ ] CI/CD pipeline
- [ ] Add missing API:s