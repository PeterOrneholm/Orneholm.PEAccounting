using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orneholm.PEAccountingNet.Filters;
using Orneholm.PEAccountingNet.Helpers;
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
            return await _httpClient.GetSingleAsync<companyinformation, CompanyInformation>("/info", CompanyInformation.FromNative);
        }

        // Users

        public async Task<User> GetMyUserAsync()
        {
            return await _httpClient.GetSingleAsync<user, User>("/user/me", User.FromNative);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _httpClient.GetListAsync<users, user, User>("/user", x => x.user, User.FromNative);
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await _httpClient.GetSingleAsync<user, User>($"/user/{userId}", User.FromNative);
        }


        // Clients

        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            return await _httpClient.GetListAsync<clients, client, Client>("/client", x => x.client, Client.FromNative);
        }

        // Projects

        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            return await _httpClient.GetListAsync<projects, project, Project>("/project", x => x.project, Project.FromNative);
        }


        // Client projects

        public async Task<IEnumerable<ClientProject>> GetClientProjectsAsync()
        {
            return await GetClientProjectsAsync(new ClientProjectFilter());
        }

        public async Task<IEnumerable<ClientProject>> GetClientProjectsAsync(ClientProjectFilter filter)
        {
            var url = QueryStringUrl.GetUrl("/client-project", filter.ToQueryStringDictionary());
            return await _httpClient.GetListAsync<clientprojectreadables, clientprojectreadable, ClientProject>(url, x => x.clientprojectreadable, ClientProject.FromNative);
        }

        public async Task<ClientProject> GetClientProjectAsync(int clientProjectId)
        {
            return await _httpClient.GetSingleAsync<clientprojectreadable, ClientProject>($"/client-project/{clientProjectId}", ClientProject.FromNative);
        }


        // Expenses

        /// <summary>
        /// Fetch all the expenses that belongs to the user, which the token is tied to
        /// </summary>
        public async Task<IEnumerable<Expense>> GetExpensesAsync()
        {
            return await GetExpensesAsync(new ExpenseFilter());
        }

        /// <summary>
        /// Search all the expenses that belongs to the user, which the token is tied to
        /// </summary>
        public async Task<IEnumerable<Expense>> GetExpensesAsync(ExpenseFilter filter)
        {
            var url = QueryStringUrl.GetUrl("/expense", filter.ToQueryStringDictionary());
            return await _httpClient.GetListAsync<expensereadables, expensereadablesExpense, Expense>(url, x => x.expense, Expense.FromNative);
        }

        // Activities

        public async Task<IEnumerable<Activity>> GetActivitiesAsync()
        {
            return await _httpClient.GetListAsync<activityreadables, activityreadable, Activity>("/activity", x => x.activityreadable, Activity.FromNative);
        }

        public async Task<Activity> GetActivityAsync(int activityId)
        {
            return await _httpClient.GetSingleAsync<activityreadable, Activity>($"/activity/{activityId}", Activity.FromNative);
        }


        // Events

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            return await GetEventsAsync(new EventFilter());
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(EventFilter filter)
        {
            var url = QueryStringUrl.GetUrl("/event", filter.ToQueryStringDictionary());
            return await _httpClient.GetListAsync<eventreadables, eventreadable, Event>(url, x => x.eventreadable, Event.FromNative);
        }

        public async Task<Event> GetEventAsync(int eventId)
        {
            return await _httpClient.GetSingleAsync<eventreadable, Event>($"/event/{eventId}", Event.FromNative);
        }

        public async Task CreateEventAsync(EventCreate eventItem)
        {
            await _httpClient.PutAsync("/event/", eventItem.ToNative());
        }

        public async Task<Deleted> DeleteEventAsync(int eventId)
        {
            return await _httpClient.DeleteAsync<deleted, Deleted>($"/event/{eventId}", Deleted.FromNative);
        }
    }
}
