using System.Collections.Generic;
using System.Linq;

namespace Orneholm.PEAccountingNet.Filters
{
    public class ClientInvoiceCreateOptions
    {
        /// <summary>
        /// Retrieves only the events related to the specified user
        /// </summary>
        public bool? DefaultDimensionsFromProduct { get; set; }

        /// <summary>
        /// Retrieves only events with date the same day or later than the specified. Default value is start of the current week.
        /// </summary>
        public bool? DefaultDimensionsFromClient { get; set; }

        public Dictionary<string, string> ToQueryStringDictionary()
        {
            return new Dictionary<string, string>
                {
                    {"defaultDimensionsFromProduct", DefaultDimensionsFromProduct?.ToString().ToLower()},
                    {"defaultDimensionsFromClient", DefaultDimensionsFromClient?.ToString().ToLower()}
                }
                .Where(x => x.Value != null)
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}