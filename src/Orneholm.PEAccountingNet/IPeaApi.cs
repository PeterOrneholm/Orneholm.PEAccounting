using System.Collections.Generic;
using System.Threading.Tasks;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet
{
    public interface IPeaApi
    {
        Task<companyinformation> GetCompanyInfoAsync();
        Task<user> GetMyUserAsync();
        Task<IEnumerable<user>> GetUsersAsync();
        Task<user> GetUserAsync(int userId);
        Task<IEnumerable<client>> GetClientsAsync();
        Task<IEnumerable<supplier>> GetSuppliersAsync();
        Task<IEnumerable<product>> GetProductsAsync();
        Task<product> GetProductAsync(int productId);
        Task<IEnumerable<project>> GetProjectsAsync();
        Task<IEnumerable<clientprojectreadable>> GetClientProjectsAsync();
        Task<clientprojectreadable> GetClientProjectAsync(int clientProjectId);

        /// <summary>
        /// Fetch all the expenses that belongs to the user, which the token is tied to
        /// </summary>
        Task<IEnumerable<expensereadablesExpense>> GetExpensesAsync();

        /// <summary>
        /// Search all the expenses that belongs to the user, which the token is tied to
        /// </summary>
        Task<IEnumerable<expensereadablesExpense>> SearchExpensesAsync(string query);

        /// <remarks>All receipts in the expense must have the same payment type (either private expense or corporate credit card expense).</remarks>
        Task<created> CreateExpenseAsync(expensewritable expense);

        Task<IEnumerable<expensefilereadablesExpensefile>> GetExpenseFilesAsync();
        Task<IEnumerable<expenseentryreadable>> GetExpenseEntriesAsync();
        Task<expenseentryreadable> GetExpenseEntryAsync(int expenseEntryId);
        Task<IEnumerable<activityreadable>> GetActivitiesAsync();
        Task<activityreadable> GetActivityAsync(int activityId);
        Task<IEnumerable<eventreadable>> GetEventsAsync();
        Task<eventreadable> GetEventAsync(int eventId);
        Task CreateEventAsync(eventwritable eventItem);
        Task<deleted> DeleteEventAsync(int eventId);
    }
}