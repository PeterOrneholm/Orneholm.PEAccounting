using System;
using System.Collections.Generic;
using System.Linq;

namespace Orneholm.PEAccountingNet.Filters
{
    public class EventFilter
    {
        /// <summary>
        /// Retrieves only the events related to the specified user
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Retrieves only events with date the same day or later than the specified. Default value is start of the current week.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Retrieves only events with the same or prior date than the one specified.Default value is the end of the current week.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Retrieves only the events related to the specified activity.
        /// </summary>
        public int? ActivityId { get; set; }

        /// <summary>
        /// Retrieves only the events related to the specific customer project.
        /// </summary>
        public int? ClientProjectId { get; set; }

        public Dictionary<string, string> ToQueryStringDictionary()
        {
            return new Dictionary<string, string>
            {
                {"userId", UserId?.ToString("D")},
                {"startDate", StartDate?.ToString("yyyy-MM-dd")},
                {"endDate", EndDate?.ToString("yyyy-MM-dd")},
                {"activityId", ActivityId?.ToString("D")},
                {"clientProjectId", ClientProjectId?.ToString("D")}
            }
            .Where(x => x.Value != null)
            .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
