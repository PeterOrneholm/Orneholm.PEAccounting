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

        public Task<CompanyInformation> GetCompanyInfoAsync()
        {
            return _httpClient.GetSingleAsync<companyinformation, CompanyInformation>("/info", CompanyInformation.FromNative);
        }

        // Users

        public Task<User> GetMyUserAsync()
        {
            return _httpClient.GetSingleAsync<user, User>("/user/me", User.FromNative);
        }

        public Task<IEnumerable<User>> GetUsersAsync()
        {
            return _httpClient.GetListAsync<users, user, User>("/user", x => x.user, User.FromNative);
        }

        public Task<User> GetUserAsync(int userId)
        {
            return _httpClient.GetSingleAsync<user, User>($"/user/{userId}", User.FromNative);
        }


        // Clients

        public Task<IEnumerable<Client>> GetClientsAsync()
        {
            return _httpClient.GetListAsync<clients, client, Client>("/client", x => x.client, Client.FromNative);
        }

        // Projects

        public Task<IEnumerable<Project>> GetProjectsAsync()
        {
            return _httpClient.GetListAsync<projects, project, Project>("/project", x => x.project, Project.FromNative);
        }


        // Client projects

        public Task<IEnumerable<ClientProject>> GetClientProjectsAsync()
        {
            return GetClientProjectsAsync(new ClientProjectFilter());
        }

        public async Task<IEnumerable<ClientProject>> GetClientProjectsAsync(ClientProjectFilter filter)
        {
            var url = QueryStringUrl.GetUrl("/client-project", filter.ToQueryStringDictionary());
            return await _httpClient.GetListAsync<clientprojectreadables, clientprojectreadable, ClientProject>(url, x => x.clientprojectreadable, ClientProject.FromNative);
        }

        public Task<ClientProject> GetClientProjectAsync(int clientProjectId)
        {
            return _httpClient.GetSingleAsync<clientprojectreadable, ClientProject>($"/client-project/{clientProjectId}", ClientProject.FromNative);
        }


        // Expenses

        /// <summary>
        /// Fetch all the expenses that belongs to the user, which the token is tied to
        /// </summary>
        public Task<IEnumerable<Expense>> GetExpensesAsync()
        {
            return GetExpensesAsync(new ExpenseFilter());
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

        public Task<IEnumerable<Activity>> GetActivitiesAsync()
        {
            return _httpClient.GetListAsync<activityreadables, activityreadable, Activity>("/activity", x => x.activityreadable, Activity.FromNative);
        }

        public Task<Activity> GetActivityAsync(int activityId)
        {
            return _httpClient.GetSingleAsync<activityreadable, Activity>($"/activity/{activityId}", Activity.FromNative);
        }


        // Events

        public Task<IEnumerable<Event>> GetEventsAsync()
        {
            return GetEventsAsync(new EventFilter());
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(EventFilter filter)
        {
            var url = QueryStringUrl.GetUrl("/event", filter.ToQueryStringDictionary());
            return await _httpClient.GetListAsync<eventreadables, eventreadable, Event>(url, x => x.eventreadable, Event.FromNative);
        }

        public Task<Event> GetEventAsync(int eventId)
        {
            return _httpClient.GetSingleAsync<eventreadable, Event>($"/event/{eventId}", Event.FromNative);
        }

        public async Task CreateEventAsync(EventCreate item)
        {
            await _httpClient.PutAsync("/event/", item.ToNative());
        }

        public Task<Deleted> DeleteEventAsync(int eventId)
        {
            return _httpClient.DeleteAsync<deleted, Deleted>($"/event/{eventId}", Deleted.FromNative);
        }


        // Client invoices

        public Task<IEnumerable<ClientInvoice>> GetClientInvoicesAsync()
        {
            return GetClientInvoicesAsync(new ClientInvoiceFilter());
        }

        public async Task<IEnumerable<ClientInvoice>> GetClientInvoicesAsync(ClientInvoiceFilter filter)
        {
            var url = QueryStringUrl.GetUrl("/client/invoice", filter.ToQueryStringDictionary());
            return await _httpClient.GetListAsync<clientinvoices, clientinvoice, ClientInvoice>(url, x => x.clientinvoice, ClientInvoice.FromNative);
        }

        public Task CreateClientInvoiceAsync(ClientInvoice item)
        {
            return CreateClientInvoiceAsync(item, new ClientInvoiceCreateOptions());
        }

        public async Task<ItemCreated> CreateClientInvoiceAsync(ClientInvoice item, ClientInvoiceCreateOptions options)
        {
            var url = QueryStringUrl.GetUrl("/client/invoice", options.ToQueryStringDictionary());
            return await _httpClient.PutAsync<clientinvoice, ItemCreated>(url, item.ToNative());
        }

        // Cluent invoice templates

        public Task<IEnumerable<ClientInvoiceTemplate>> GetClientInvoiceTempatesAsync()
        {
            return _httpClient.GetListAsync<clientinvoicetemplates, clientinvoicetemplate, ClientInvoiceTemplate>("/client/invoice/template", x => x.clientinvoicetemplate, ClientInvoiceTemplate.FromInternal);
        }

        // Client delivery types

        public Task<IEnumerable<ClientDeliveryType>> GetClientDeliveryTypesAsync()
        {
            return _httpClient.GetListAsync<clientdeliverytypes, clientdeliverytypesClientdeliverytype, ClientDeliveryType>("/client/deliverytype", x => x.clientdeliverytype, ClientDeliveryType.FromInternal);
        }
    }
}
