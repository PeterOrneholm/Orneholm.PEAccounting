using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orneholm.PEAccountingNet.Models;

namespace Orneholm.PEAccountingNet.ConsoleAppSamle
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            Console.WriteLine("PEAccountingNet API Testbench");
            Console.WriteLine("##############################");
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Login:");
            Console.WriteLine("-------------------------------");
            var companies = (await GetCompaniesAsync()).ToList();
            await PlotSectionAsync("You have access to",
                () => Task.FromResult(companies.AsEnumerable()),
                x => $"{x.Name} ({x.Id}): {x.Token}");

            var mainCompany = companies.First(x => x.IsMain);
            var api = new PeaApi(mainCompany.Id, mainCompany.Token);

            await PlotSectionAsync("Users",
                () => api.GetUsersAsync(),
                x => $"{x.Name} ({x.Id}): {x.Email}");

            await PlotSectionAsync("Projects",
                () => api.GetProjectsAsync(),
                x => $"{x.Name} ({x.Id}): {x.Description}");

            await PlotSectionAsync("Clients",
                () => api.GetClientsAsync(),
                x => $"{x.Name} ({x.Id}): {x.OrgNo}");

            await PlotSectionAsync("Client projects",
                () => api.GetClientProjectsAsync(),
                x => $"{x.Name} ({x.Id}): {x.Comment}");

            await PlotSectionAsync("Activities",
                () => api.GetActivitiesAsync(),
                x => $"{x.Name} ({x.Id}): {x.Description}");

            await PlotSectionAsync("Events",
                () => api.GetEventsAsync(),
                x => $"{x.Date} ({x.Id}): {x.Hours} h, {x.Comment}");

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

            var authenticationApi = new PeaAuthenticationApi();
            var companies = await authenticationApi.GetAccessibleCompaniesAsync(username, password);

            return companies;
        }

        private static async Task PlotSectionAsync<T>(string header, Func<Task<IEnumerable<T>>> getItems, Func<T, string> getItemString)
        {
            Console.WriteLine();
            Console.WriteLine($"{header}:");
            Console.WriteLine("-------------------------------");
            var items = await getItems();
            foreach (var item in items)
            {
                Console.WriteLine(getItemString(item));
            }
        }
    }
}
