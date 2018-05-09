using System.Collections.Generic;
using System.Linq;

namespace Orneholm.PEAccountingNet.Filters
{
    public class ExpenseFilter
    {
        public string Query { get; set; }

        public Dictionary<string, string> ToQueryStringDictionary()
        {
            return new Dictionary<string, string>
            {
                {"query", Query}
            }
            .Where(x => x.Value != null)
            .ToDictionary(x => x.Key, x => x.Value);
        }
    }

    public partial class ClientProjectFilter
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
