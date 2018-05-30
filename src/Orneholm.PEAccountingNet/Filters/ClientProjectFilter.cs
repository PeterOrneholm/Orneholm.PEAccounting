using System.Collections.Generic;
using System.Linq;

namespace Orneholm.PEAccountingNet.Filters
{
    public class ClientProjectFilter
    {
        public ClientProjectActiveStatus? Status { get; set; }

        public Dictionary<string, string> ToQueryStringDictionary()
        {
            return new Dictionary<string, string>
                {
                    {"filter", Status?.ToString().ToLower()}
                }
                .Where(x => x.Value != null)
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}