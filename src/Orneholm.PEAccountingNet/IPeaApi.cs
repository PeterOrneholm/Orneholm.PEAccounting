using System.Collections.Generic;
using System.Threading.Tasks;
using Orneholm.PEAccountingNet.Models;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet
{
    public interface IPeaApi
    {
        Task<CompanyInformation> GetCompanyInfoAsync();
        Task<User> GetMyUserAsync();
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(int userId);
        Task<IEnumerable<Client>> GetClientsAsync();
        Task<IEnumerable<Project>> GetProjectsAsync();
        Task<IEnumerable<ClientProject>> GetClientProjectsAsync();
        Task<ClientProject> GetClientProjectAsync(int clientProjectId);

        /// <summary>
        /// Fetch all the expenses that belongs to the user, which the token is tied to
        /// </summary>
        Task<IEnumerable<Expense>> GetExpensesAsync();

        /// <summary>
        /// Search all the expenses that belongs to the user, which the token is tied to
        /// </summary>
        Task<IEnumerable<Expense>> SearchExpensesAsync(string query);

        Task<IEnumerable<activityreadable>> GetActivitiesAsync();
        Task<activityreadable> GetActivityAsync(int activityId);
        Task<IEnumerable<eventreadable>> GetEventsAsync();
        Task<eventreadable> GetEventAsync(int eventId);
        Task CreateEventAsync(eventwritable eventItem);
        Task<Deleted> DeleteEventAsync(int eventId);
    }
}