using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orneholm.PEAccountingNet.Filters;
using Orneholm.PEAccountingNet.Models;

namespace Orneholm.PEAccountingNet.ConsoleSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var companyId = 0;
            var companyToken = string.Empty;

            if (args.Length == 2)
            {
                int.TryParse(args[0], out companyId);
                companyToken = args[1];
            }

            await App(companyId, companyToken);
        }

        static async Task App(int companyId, string companyToken)
        {
            Console.WriteLine("PEAccountingNet API Testbench");
            Console.WriteLine("##############################");

            if (companyId <= 0 || string.IsNullOrWhiteSpace(companyToken))
            {
                PlotHeader("Login");
                var companies = (await GetCompaniesAsync()).ToList();
                await PlotSectionAsync("You have access to",
                    () => Task.FromResult(companies.AsEnumerable()),
                    x => $"{x.Name} ({x.Id}): {x.Token}");

                var mainCompany = companies.First(x => x.IsMain);
                companyId = mainCompany.Id;
                companyToken = mainCompany.Token;
            }
            var api = PeaApi.CreateClient(companyId, companyToken);

            //await CreateInvoice(api, "123");

            PlotHeader("Me");
            var myUser = await api.GetMyUserAsync();
            Console.WriteLine($"{myUser.Name} ({myUser.Id}): {myUser.Email}");

            await PlotSectionAsync("Users",
                () => api.GetUsersAsync(),
                x => $"{x.Name} ({x.Id}): {x.Email}");

            await PlotSectionAsync("Projects",
                () => api.GetProjectsAsync(),
                x => $"{x.Name} ({x.Id}): {x.Description}");

            await PlotSectionAsync("Activities",
                () => api.GetActivitiesAsync(),
                x => $"{x.Name} ({x.Id}): {x.Description}");

            await PlotSectionAsync("Events",
                () => api.GetEventsAsync(),
                x => $"{x.Date} ({x.Id}): {x.Hours} h, Comment: {x.Comment}, Internal comment: {x.InternalComment}, Child: {x.Child}");

            await PlotSectionAsync("Clients",
                () => api.GetClientsAsync(),
                x => $"{x.Name} ({x.Id}): {x.OrgNo}");

            await PlotSectionAsync("Client projects",
                () => api.GetClientProjectsAsync(),
                x => $"{x.Name} ({x.Id}): {x.Comment}");

            await PlotSectionAsync("Client delivery types",
                () => api.GetClientDeliveryTypesAsync(),
                x => $"{x.Name}: IsDefault: {x.IsDefault}");

            await PlotSectionAsync("Client invoice templates",
                () => api.GetClientInvoiceTempatesAsync(),
                x => $"{x.Name} ({x.Id}): {x.Description}");

            await PlotSectionAsync("Products",
                () => api.GetProductsAsync(),
                x => $"{x.Name} ({x.Id}): Price: {x.Price}");

            await PlotSectionAsync("Dimensions",
                () => api.GetDimensionsAsync(),
                x => $"{x.Name} ({x.Id}): {x.Description}");

            var dimensions = (await api.GetDimensionsAsync()).ToList();
            if (dimensions.Any())
            {
                await PlotSectionAsync("Dimension entries (for first dimension)",
                    () => api.GetDimensionEntriesAsync(dimensions.First().Id),
                    x => $"{x.Name} ({x.Id}): {x.Description}");
            }

            var now = DateTime.UtcNow;
            var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            await PlotSectionAsync("Your events current month",
                () => api.GetEventsAsync(new EventFilter()
                {
                    UserId = myUser.Id,
                    StartDate = firstDayOfMonth,
                    EndDate = lastDayOfMonth
                }),
                x => $"{x.Date} ({x.Id}): {x.Hours} h, Comment: {x.Comment}, Internal comment: {x.InternalComment}, Child: {x.Child}");

            await PlotSectionAsync("Expenses",
                () => api.GetExpensesAsync(),
                x => $"{x.Date} ({x.Id}): {x.Amount} {x.CurrencyType}, Nr: {x.Nr}");

            Console.ReadLine();
        }

        private static async Task<IEnumerable<AccessibleCompany>> GetCompaniesAsync()
        {
            Console.WriteLine("Username:");
            var username = Console.ReadLine();

            Console.WriteLine("Password:");
            var password = Console.ReadLine();

            var authenticationApi = PeaAuthenticationApi.CreateClient();
            var companies = await authenticationApi.GetAccessibleCompaniesAsync(username, password);

            return companies;
        }

        private static async Task PlotSectionAsync<T>(string header, Func<Task<IEnumerable<T>>> getItems, Func<T, string> getItemString)
        {
            PlotHeader(header);
            var items = await getItems();
            foreach (var item in items)
            {
                Console.WriteLine(getItemString(item));
            }
        }

        private static void PlotHeader(string header)
        {
            Console.WriteLine();
            Console.WriteLine($"{header}:");
            Console.WriteLine("-------------------------------");
        }


        private static async Task CreateInvoice(IPeaApi api, string clientForeignId)
        {
            var clients = await api.GetClientsAsync();
            var client = clients.First(x => x.ForeignId == clientForeignId);

            var now = DateTime.UtcNow;
            var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var invoice = new ClientInvoiceCreate
            {
                ClientId = client.Id,
                InvoiceDate = firstDayOfMonth,
                DueDate = lastDayOfMonth,

                Currency = CurrencyConstants.Sek,

                DeliveryDate = firstDayOfMonth,
                DeliveryType = ClientDeliveryTypeConstants.Email,

                InvoiceAddress = client.Address,
                InvoiceEmail = client.Email,

                YourReference = client.Contact,

                Notes = "Generated by API",

                Rows =
                {
                    new ClientInvoiceRowCreate()
                    {
                        
                    }
                }
            };

            var result = await api.CreateClientInvoiceAsync(invoice);
            Console.WriteLine($"Client invoice created: {result.Id}");
        }
    }
}
