using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orneholm.PEAccountingNet.Models;
using Orneholm.PEAccountingNet.Models.Native;

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

        public async Task<CompanyInformation> GetCompanyInfoAsync()
        {
            return await GetItemResultAsync<companyinformation, CompanyInformation>("/info", CompanyInformation.FromNative);
        }

        // Users

        public async Task<User> GetMyUserAsync()
        {
            return await GetItemResultAsync<user, User>("/user/me", User.FromNative);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await GetListResultAsync<users, user, User>("/user", x => x.user, User.FromNative);
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await GetItemResultAsync<user, User>($"/user/{userId}", User.FromNative);
        }


        // Clients

        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            return await GetListResultAsync<clients, client, Client>("/client", x => x.client, Client.FromNative);
        }

        // Projects

        public async Task<IEnumerable<project>> GetProjectsAsync()
        {
            return await GetListResultAsync<projects, project>("/project", x => x.project);
        }


        // Client projects

        public async Task<IEnumerable<clientprojectreadable>> GetClientProjectsAsync()
        {
            return await GetListResultAsync<clientprojectreadables, clientprojectreadable>("/client-project", x => x.clientprojectreadable);
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
            return await GetListResultAsync<expensereadables, expensereadablesExpense>("/expense", x => x.expense);
        }

        /// <summary>
        /// Search all the expenses that belongs to the user, which the token is tied to
        /// </summary>
        public async Task<IEnumerable<expensereadablesExpense>> SearchExpensesAsync(string query)
        {
            return await GetListResultAsync<expensereadables, expensereadablesExpense>($"/expense?query=¨{Uri.EscapeDataString(query)}", x => x.expense);
        }

        /// <remarks>All receipts in the expense must have the same payment type (either private expense or corporate credit card expense).</remarks>
        public async Task<created> CreateExpenseAsync(expensewritable expense)
        {
            return await _httpClient.PutAsync<expensewritable, created>("/expense/", expense);
        }

        public async Task<IEnumerable<expensefilereadablesExpensefile>> GetExpenseFilesAsync()
        {
            return await GetListResultAsync<expensefilereadables, expensefilereadablesExpensefile>("/expense/file/open", x => x.expensefile);
        }

        public async Task<IEnumerable<expenseentryreadable>> GetExpenseEntriesAsync()
        {
            return await GetListResultAsync<expenseentryreadables, expenseentryreadable>("/expense/entry/open", x => x.expenseentry);
        }

        public async Task<expenseentryreadable> GetExpenseEntryAsync(int expenseEntryId)
        {
            return await _httpClient.GetAsync<expenseentryreadable>($"/expense/entry/expense/{expenseEntryId}");
        }


        // Activities

        public async Task<IEnumerable<activityreadable>> GetActivitiesAsync()
        {
            return await GetListResultAsync<activityreadables, activityreadable>("/activity", x => x.activityreadable);
        }

        public async Task<activityreadable> GetActivityAsync(int activityId)
        {
            return await _httpClient.GetAsync<activityreadable>($"/activity/{activityId}");
        }


        // Events

        public async Task<IEnumerable<eventreadable>> GetEventsAsync()
        {
            return await GetListResultAsync<eventreadables, eventreadable>("/event", x => x.eventreadable);
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

        private async Task<TItem> GetItemResultAsync<TNativeItem, TItem>(string url, Func<TNativeItem, TItem> transformItem)
        {
            var result = await _httpClient.GetAsync<TNativeItem>(url);
            if (result == null)
            {
                return default(TItem);
            }

            return transformItem(result);
        }

        private async Task<IEnumerable<TNativeItem>> GetListResultAsync<TResult, TNativeItem>(string url, Func<TResult, IEnumerable<TNativeItem>> getValue)
        {
            var result = await _httpClient.GetAsync<TResult>(url);
            return PeaApiHelpers.TransformListResult(result, getValue);
        }

        private async Task<IEnumerable<TItem>> GetListResultAsync<TResult, TNativeItem, TItem>(string url, Func<TResult, IEnumerable<TNativeItem>> getValue, Func<TNativeItem, TItem> transformItem)
        {
            var result = await _httpClient.GetAsync<TResult>(url);
            return PeaApiHelpers.TransformListResult(result, getValue, transformItem);
        }
    }
}
