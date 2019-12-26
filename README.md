# Orneholm.PEAccountingNet

[![License: MIT](https://img.shields.io/badge/License-MIT-orange.svg)](https://opensource.org/licenses/MIT)
[![Build Status](https://dev.azure.com/orneholm/Orneholm.PEAccounting/_apis/build/status/Orneholm.PEAccounting?branchName=master)](https://dev.azure.com/orneholm/Orneholm.PEAccounting/_build/latest?definitionId=4&branchName=master)
[![NuGet](https://img.shields.io/nuget/v/Orneholm.PEAccountingNet.svg)](https://www.nuget.org/packages/Orneholm.PEAccountingNet/)
[![Twitter Follow](https://img.shields.io/badge/Twitter-@PeterOrneholm-blue.svg?logo=twitter)](https://twitter.com/PeterOrneholm)

Unofficial wrapper of the [p:e accounting](https://www.accounting.pe/) API for .NET. Built in C# targeting .NET Standard 2.0. Based on the public [documentation](https://my.accounting.pe/api/v1/doc).

## Features

- :chart_with_upwards_trend: Typed wrappers for the PE Accounting API
- :penguin: Cross platform: Targets .NET Standard 2.0 (Use from .NET Framework and .NET Core)

## Supported methods

The API-wrapper supports all these methods:

- Company
    - `GetCompanyInfoAsync()`
- User
    - `GetMyUserAsync`
    - `GetUsersAsync()`
    - `GetUserAsync(int userId)`
- Clients
    - `GetClientsAsync()`
- Projects
    - `GetProjectsAsync()`
- Client projects
    - `GetClientProjectsAsync()`
    - `GetClientProjectsAsync(ClientProjectFilter filter)`
    - `GetClientProjectAsync(int clientProjectId)`
- Expenses
    - `GetExpensesAsync()
    - `GetExpensesAsync(ExpenseFilter filter)`
- Activities
    - `GetActivitiesAsync()`
    - `GetActivityAsync(int activityId)`
- Events
    - `GetEventsAsync()`
    - `GetEventsAsync(EventFilter filter)`
    - `GetEventAsync(int eventId)`
    - `CreateEventAsync(EventCreate item)`
    - `DeleteEventAsync(int eventId)`
- Client invoices
    - `GetClientInvoicesAsync()`
    - `GetClientInvoicesAsync(ClientInvoiceFilter filter)`
    - `CreateClientInvoiceAsync(ClientInvoiceCreate item)`
    - `CreateClientInvoiceAsync(ClientInvoiceCreate item, ClientInvoiceCreateOptions options)`
- Client invoice templates
    - `GetClientInvoiceTempatesAsync()`
- Client delivery types
    - `GetClientDeliveryTypesAsync()`
- Dimensions
    - `GetDimensionsAsync()
    - `GetDimensionEntriesAsync(int dimensionId)`
- Products
    - `GetProductsAsync()`
    - `GetProductAsync(int productId)`

## Getting started

### 1. Read the documentation

Please start by reading the official documentation to get a basic understanding of the API:
https://my.accounting.pe/api/v1/doc

### 2. Install the NuGet package

Orneholm.PEAccountingNet is distributed as [packages on NuGet](https://www.nuget.org/profiles/PeterOrneholm), install using the tool of your choice, for example _dotnet cli_.

```console
dotnet add package Orneholm.PEAccountingNet
```

### 3. Use the API

### Authentication API

The main API is scoped to a single company and restricted to the access rights of a single user given a personal access token.
The `PeaAuthenticationApi` provides a simple way of retrieving those companies and tokens given a _username_ and _password_.

*Example:*
```csharp
var authenticationApi = PeaAuthenticationApi.CreateClient();
var companies = await authenticationApi.GetAccessibleCompaniesAsync("username", "password");
foreach (var company in companies)
{
   Console.WriteLine($"{company.Name} ({company.Id}): {company.Token}");
}
```

### Data API

*Example:*
```csharp
var api = PeaApi.CreateClient(company.Id, company.Token);

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

// Create event
await api.CreateEventAsync(new EventCreate()
{
    Date = DateTime.Today.Date,
    Hours = 1,

    UserId = 12345,
    ActivityId = 67890,
    ClientProjectId = 13579,

    Comment = "Doing work",
    InternalComment = "Work"
});
```

## Models

The native models used to map the XML requests and responses are not exposed through the API, but instead beeing mapped against custom models.
This enabled the API to follow the conventions of C# but also makes sure that any breaking change in the models is beeing noticed.

### Regenerating models
The models are auto generated using [xsd.exe](https://docs.microsoft.com/en-us/dotnet/standard/serialization/xml-schema-definition-tool-xsd-exe) with the [XSD source from p:e accoutning](https://my.accounting.pe/api/v1/xsd).

To generate new classes, download a new version of the XSD, place it in /Models/Native and run the following command from [Developer Command Prompt for Visual Studio](https://docs.microsoft.com/en-us/dotnet/framework/tools/developer-command-prompt-for-vs).

```console
xsd NativeModels.xsd /c /n:Orneholm.PEAccountingNet.Models.Native
```

---

## Samples & Test

For more use cases, samples and inspiration; feel free to browse our sample.

- [Orneholm.PEAccountingNet.ConsoleSample](samples/Orneholm.PEAccountingNet.ConsoleSample)

## Contribute

We are very open to community contributions.
Please see our [contribution guidelines](CONTRIBUTING.md) before getting started.

### License & acknowledgements

Orneholm.PEAccountingNet is licensed under the very permissive [MIT license](https://opensource.org/licenses/MIT) for you to be able to use it in commercial or non-commercial applications without many restrictions.

The brand PE Accounting belongs to PE Accounting.
