using System;
using System.Collections.Generic;
using System.Net.Http;
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

        public PeaApi(int companyId, HttpClient httpClient)
        {
            var apiHttpClient = new PeaApiHttpClient(httpClient);
            _httpClient = new PeaApiCompanyHttpClient(companyId, apiHttpClient);
        }

        private PeaApi(int companyId, IPeaApiHttpClient httpClient)
        {
            _httpClient = new PeaApiCompanyHttpClient(companyId, httpClient);
        }

        public static PeaApi CreateClient(int companyId, string accessToken)
        {
            return new PeaApi(companyId, PeaApiHttpClient.CreateClient(accessToken));
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

        public Task<ItemCreated> CreateClientInvoiceAsync(ClientInvoiceCreate item)
        {
            return CreateClientInvoiceAsync(item, new ClientInvoiceCreateOptions());
        }

        public async Task<ItemCreated> CreateClientInvoiceAsync(ClientInvoiceCreate item, ClientInvoiceCreateOptions options)
        {
            var url = QueryStringUrl.GetUrl("/client/invoice", options.ToQueryStringDictionary());
            return await _httpClient.PutAsync<clientinvoice, ItemCreated>(url, item.ToNative());
        }

        // Cluent invoice templates

        public Task<IEnumerable<ClientInvoiceTemplate>> GetClientInvoiceTempatesAsync()
        {
            return _httpClient.GetListAsync<clientinvoicetemplates, clientinvoicetemplate, ClientInvoiceTemplate>("/client/invoice/template", x => x.clientinvoicetemplate, ClientInvoiceTemplate.FromNative);
        }

        // Client delivery types

        public Task<IEnumerable<ClientDeliveryType>> GetClientDeliveryTypesAsync()
        {
            return _httpClient.GetListAsync<clientdeliverytypes, clientdeliverytypesClientdeliverytype, ClientDeliveryType>("/client/deliverytype", x => x.clientdeliverytype, ClientDeliveryType.FromNative);
        }

        // Dimensions


        /// <summary>
        /// Dimensions (categories)
        /// </summary>
        public Task<IEnumerable<Dimension>> GetDimensionsAsync()
        {
            return _httpClient.GetListAsync<dimensions, dimension, Dimension>("/dimension", x => x.dimension, Dimension.FromNative);
        }

        /// <summary>
        /// Entries for dimensions (categories)
        /// </summary>
        public Task<IEnumerable<DimensionEntry>> GetDimensionEntriesAsync(int dimensionId)
        {
            return _httpClient.GetListAsync<dimensionentries, dimensionentry, DimensionEntry>($"/dimension/{dimensionId}/entry", x => x.dimensionentry, DimensionEntry.FromNative);
        }


        // Products

        public Task<IEnumerable<Product>> GetProductsAsync()
        {
            return _httpClient.GetListAsync<products, product, Product>("/product", x => x.product, Product.FromNative);
        }

        public Task<Product> GetProductAsync(int productId)
        {
            return _httpClient.GetSingleAsync<product, Product>($"/product/{productId}", Product.FromNative);
        }

        /// <summary>
        /// Fetch all general ledger accounts
        /// </summary>
        public Task<IEnumerable<Account>> GetGeneralLedgerAccountsAsync(DateTime startDate, DateTime endDate)
        {
            return _httpClient.GetListAsync<accountmetadatas, accountmetadata, Account>($"/accounting/account/{startDate:yyyy-MM-dd}/{endDate:yyyy-MM-dd}/metadata", x => x.account, Account.FromNative);
        }
    }
}
