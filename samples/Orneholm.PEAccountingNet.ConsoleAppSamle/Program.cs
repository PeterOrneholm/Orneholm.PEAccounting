using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orneholm.PEAccountingNet.Models;
using Orneholm.PEAccountingNet.Models.Native;

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
            var mainCompany = companies.First(x => x.IsMain);

            Console.WriteLine();
            Console.WriteLine("You have access to:");
            Console.WriteLine("-------------------------------");
            foreach (var company in companies)
            {
                Console.WriteLine($"{company.Name} ({company.Id}): {company.Token}");
            }

            var api = new PeaApi(mainCompany.Id, mainCompany.Token);

            await PlotClientsAsync(api);
            await PlotClientProjectsAsync(api);
            await PlotActivitiesAsync(api);

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
        private static async Task PlotClientsAsync(IPeaApi api)
        {
            Console.WriteLine();
            Console.WriteLine("Clients:");
            Console.WriteLine("-------------------------------");
            var result = await api.GetClientsAsync();
            foreach (var item in result)
            {
                Console.WriteLine($"{item.Name} ({item.Id}): {item.OrgNo}");
            }
        }

        private static async Task PlotClientProjectsAsync(IPeaApi api)
        {
            Console.WriteLine();
            Console.WriteLine("Client projects:");
            Console.WriteLine("-------------------------------");
            var result = await api.GetClientProjectsAsync();
            foreach (var item in result)
            {
                Console.WriteLine($"{item.Name} ({item.Id}): {item.Comment}");
            }
        }

        private static async Task PlotActivitiesAsync(IPeaApi api)
        {
            Console.WriteLine();
            Console.WriteLine("Activities:");
            Console.WriteLine("-------------------------------");
            var result = await api.GetActivitiesAsync();
            foreach (var item in result)
            {
                Console.WriteLine($"{item.name} ({item.id.id}): {item.description}");
            }
        }
    }
}
