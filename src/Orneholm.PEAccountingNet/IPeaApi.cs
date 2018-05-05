using System.Collections.Generic;
using System.Threading.Tasks;
using Orneholm.PEAccountingNet.Models;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet
{
    public interface IPeaApi
    {
        // Company

        Task<CompanyInformation> GetCompanyInfoAsync();

        //User

        Task<User> GetMyUserAsync();
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(int userId);

        // Clients

        Task<IEnumerable<Client>> GetClientsAsync();

        // Projects

        Task<IEnumerable<Project>> GetProjectsAsync();

        // Client projects

        Task<IEnumerable<ClientProject>> GetClientProjectsAsync();
        Task<ClientProject> GetClientProjectAsync(int clientProjectId);

        // Expenses

        /// <summary>
        /// Fetch all the expenses that belongs to the user, which the token is tied to
        /// </summary>
        Task<IEnumerable<Expense>> GetExpensesAsync();

        /// <summary>
        /// Search all the expenses that belongs to the user, which the token is tied to
        /// </summary>
        Task<IEnumerable<Expense>> SearchExpensesAsync(string query);

        // Activities

        Task<IEnumerable<Activity>> GetActivitiesAsync();
        Task<Activity> GetActivityAsync(int activityId);

        // Events

        Task<IEnumerable<Event>> GetEventsAsync();
        Task<Event> GetEventAsync(int eventId);
        Task CreateEventAsync(EventCreate eventItem);
        Task<Deleted> DeleteEventAsync(int eventId);
    }
}