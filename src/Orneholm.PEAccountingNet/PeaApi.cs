using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orneholm.PEAccountingNet.Models;

namespace Orneholm.PEAccountingNet
{
    public class PeaApi : IPeaApi
    {
        private readonly IPeaApiHttpClient _httpClient;

        public PeaApi(int companyId, string accessToken)
            : this(companyId, new PeaApiHttpClient(accessToken))
        {
        }

        public PeaApi(int companyId, IPeaApiHttpClient httpClient)
        {
            _httpClient = new PeaApiCompanyHttpClient(companyId, httpClient);
        }

        // Company

        public async Task<companyinformation> GetCompanyInfoAsync()
        {
            return await _httpClient.GetAsync<companyinformation>("/info");
        }

        // Users

        public async Task<user> GetMyUserAsync()
        {
            return await _httpClient.GetAsync<user>("/user/me");
        }

        public async Task<IEnumerable<user>> GetUsersAsync()
        {
            return await GetCompanyListResultAsync<users, user>("/user", x => x.user);
        }

        public async Task<user> GetUserAsync(int userId)
        {
            return await _httpClient.GetAsync<user>($"/user/{userId}");
        }


        // Clients

        public async Task<IEnumerable<client>> GetClientsAsync()
        {
            return await GetCompanyListResultAsync<clients, client>("/client", x => x.client);
        }


        // Suppliers

        public async Task<IEnumerable<supplier>> GetSuppliersAsync()
        {
            return await GetCompanyListResultAsync<suppliers, supplier>("/supplier", x => x.supplier);
        }


        // Products

        public async Task<IEnumerable<product>> GetProductsAsync()
        {
            return await GetCompanyListResultAsync<products, product>("/product", x => x.product);
        }

        public async Task<product> GetProductAsync(int productId)
        {
            return await _httpClient.GetAsync<product>($"/product/{productId}");
        }


        // Projects

        public async Task<IEnumerable<project>> GetProjectsAsync()
        {
            return await GetCompanyListResultAsync<projects, project>("/project", x => x.project);
        }


        // Client projects

        public async Task<IEnumerable<clientprojectreadable>> GetClientProjectsAsync()
        {
            return await GetCompanyListResultAsync<clientprojectreadables, clientprojectreadable>("/client-project", x => x.clientprojectreadable);
        }

        public async Task<clientprojectreadable> GetClientProjectAsync(int clientProjectId)
        {
            return await _httpClient.GetAsync<clientprojectreadable>($"/client-project/{clientProjectId}");
        }


        // Expenses

        /// <summary>
        /// Fetch all the expenses that belongs to the user, which the token is tied to
        /// </summary>
        public async Task<IEnumerable<expensereadablesExpense>> GetExpensesAsync()
        {
            return await GetCompanyListResultAsync<expensereadables, expensereadablesExpense>("/expense", x => x.expense);
        }

        /// <summary>
        /// Search all the expenses that belongs to the user, which the token is tied to
        /// </summary>
        public async Task<IEnumerable<expensereadablesExpense>> SearchExpensesAsync(string query)
        {
            return await GetCompanyListResultAsync<expensereadables, expensereadablesExpense>($"/expense?query=¨{Uri.EscapeDataString(query)}", x => x.expense);
        }

        /// <remarks>All receipts in the expense must have the same payment type (either private expense or corporate credit card expense).</remarks>
        public async Task<created> CreateExpenseAsync(expensewritable expense)
        {
            return await _httpClient.PutAsync<expensewritable, created>("/expense/", expense);
        }

        public async Task<IEnumerable<expensefilereadablesExpensefile>> GetExpenseFilesAsync()
        {
            return await GetCompanyListResultAsync<expensefilereadables, expensefilereadablesExpensefile>("/expense/file/open", x => x.expensefile);
        }

        public async Task<IEnumerable<expenseentryreadable>> GetExpenseEntriesAsync()
        {
            return await GetCompanyListResultAsync<expenseentryreadables, expenseentryreadable>("/expense/entry/open", x => x.expenseentry);
        }

        public async Task<expenseentryreadable> GetExpenseEntryAsync(int expenseEntryId)
        {
            return await _httpClient.GetAsync<expenseentryreadable>($"/expense/entry/expense/{expenseEntryId}");
        }


        // Activities

        public async Task<IEnumerable<activityreadable>> GetActivitiesAsync()
        {
            return await GetCompanyListResultAsync<activityreadables, activityreadable>("/activity", x => x.activityreadable);
        }

        public async Task<activityreadable> GetActivityAsync(int activityId)
        {
            return await _httpClient.GetAsync<activityreadable>($"/activity/{activityId}");
        }


        // Events

        public async Task<IEnumerable<eventreadable>> GetEventsAsync()
        {
            return await GetCompanyListResultAsync<eventreadables, eventreadable>("/event", x => x.eventreadable);
        }

        public async Task<eventreadable> GetEventAsync(int eventId)
        {
            return await _httpClient.GetAsync<eventreadable>($"/event/{eventId}");
        }

        public async Task CreateEventAsync(eventwritable eventItem)
        {
            await _httpClient.PutAsync("/event/", eventItem);
        }

        public async Task<deleted> DeleteEventAsync(int eventId)
        {
            return await _httpClient.DeleteAsync<deleted>($"/event/{eventId}");
        }


        // Helpers

        private async Task<IEnumerable<TItem>> GetCompanyListResultAsync<TResult, TItem>(string url, Func<TResult, IEnumerable<TItem>> projection)
        {
            var result = await _httpClient.GetAsync<TResult>(url);
            return PeaApiHelpers.TransformListResult(result, projection);
        }
    }
}
