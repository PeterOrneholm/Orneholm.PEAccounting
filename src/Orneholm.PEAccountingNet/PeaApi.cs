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
            return await GetSingleAsync<companyinformation, CompanyInformation>("/info", CompanyInformation.FromNative);
        }

        // Users

        public async Task<User> GetMyUserAsync()
        {
            return await GetSingleAsync<user, User>("/user/me", User.FromNative);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await GetListAsync<users, user, User>("/user", x => x.user, User.FromNative);
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await GetSingleAsync<user, User>($"/user/{userId}", User.FromNative);
        }


        // Clients

        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            return await GetListAsync<clients, client, Client>("/client", x => x.client, Client.FromNative);
        }

        // Projects

        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            return await GetListAsync<projects, project, Project>("/project", x => x.project, Project.FromNative);
        }


        // Client projects

        public async Task<IEnumerable<ClientProject>> GetClientProjectsAsync()
        {
            return await GetListAsync<clientprojectreadables, clientprojectreadable, ClientProject>("/client-project", x => x.clientprojectreadable, ClientProject.FromNative);
        }

        public async Task<ClientProject> GetClientProjectAsync(int clientProjectId)
        {
            return await GetSingleAsync<clientprojectreadable, ClientProject>($"/client-project/{clientProjectId}", ClientProject.FromNative);
        }


        // Expenses

        /// <summary>
        /// Fetch all the expenses that belongs to the user, which the token is tied to
        /// </summary>
        public async Task<IEnumerable<expensereadablesExpense>> GetExpensesAsync()
        {
            return await GetListAsync<expensereadables, expensereadablesExpense>("/expense", x => x.expense);
        }

        /// <summary>
        /// Search all the expenses that belongs to the user, which the token is tied to
        /// </summary>
        public async Task<IEnumerable<expensereadablesExpense>> SearchExpensesAsync(string query)
        {
            return await GetListAsync<expensereadables, expensereadablesExpense>($"/expense?query=¨{Uri.EscapeDataString(query)}", x => x.expense);
        }

        // Activities

        public async Task<IEnumerable<activityreadable>> GetActivitiesAsync()
        {
            return await GetListAsync<activityreadables, activityreadable>("/activity", x => x.activityreadable);
        }

        public async Task<activityreadable> GetActivityAsync(int activityId)
        {
            return await _httpClient.GetAsync<activityreadable>($"/activity/{activityId}");
        }


        // Events

        public async Task<IEnumerable<eventreadable>> GetEventsAsync()
        {
            return await GetListAsync<eventreadables, eventreadable>("/event", x => x.eventreadable);
        }

        public async Task<eventreadable> GetEventAsync(int eventId)
        {
            return await _httpClient.GetAsync<eventreadable>($"/event/{eventId}");
        }

        public async Task CreateEventAsync(eventwritable eventItem)
        {
            await _httpClient.PutAsync("/event/", eventItem);
        }

        public async Task<Deleted> DeleteEventAsync(int eventId)
        {
            return await DeleteAsync<deleted, Deleted>($"/event/{eventId}", Deleted.FromNative);
        }


        // Helpers

        private async Task<TItem> GetSingleAsync<TNativeItem, TItem>(string url, Func<TNativeItem, TItem> transformItem)
        {
            var result = await _httpClient.GetAsync<TNativeItem>(url);
            if (result == null)
            {
                return default(TItem);
            }

            return transformItem(result);
        }

        private async Task<TItem> PutAsync<TRequest, TNativeItem, TItem>(string url, TRequest request, Func<TNativeItem, TItem> transformItem)
        {
            var result = await _httpClient.PutAsync<TRequest, TNativeItem>(url, request);
            if (result == null)
            {
                return default(TItem);
            }

            return transformItem(result);
        }

        private async Task<TItem> Post<TRequest, TNativeItem, TItem>(string url, TRequest request, Func<TNativeItem, TItem> transformItem)
        {
            var result = await _httpClient.PostAsync<TRequest, TNativeItem>(url, request);
            if (result == null)
            {
                return default(TItem);
            }

            return transformItem(result);
        }

        private async Task<TItem> DeleteAsync<TNativeItem, TItem>(string url, Func<TNativeItem, TItem> transformItem)
        {
            var result = await _httpClient.DeleteAsync<TNativeItem>(url);
            if (result == null)
            {
                return default(TItem);
            }

            return transformItem(result);
        }

        private async Task<IEnumerable<TNativeItem>> GetListAsync<TResult, TNativeItem>(string url, Func<TResult, IEnumerable<TNativeItem>> getValue)
        {
            var result = await _httpClient.GetAsync<TResult>(url);
            return PeaApiHelpers.TransformListResult(result, getValue);
        }

        private async Task<IEnumerable<TItem>> GetListAsync<TResult, TNativeItem, TItem>(string url, Func<TResult, IEnumerable<TNativeItem>> getValue, Func<TNativeItem, TItem> transformItem)
        {
            var result = await _httpClient.GetAsync<TResult>(url);
            return PeaApiHelpers.TransformListResult(result, getValue, transformItem);
        }
    }
}
