using System;
using System.Collections.Generic;
using System.Linq;

namespace Orneholm.PEAccountingNet.Filters
{
    public class ClientInvoiceFilter
    {
        public ClientInvoiceStatus? Status { get; set; }
        /// <summary>
        /// Free-text search.
        /// </summary>
        public string Query { get; set; }
        /// <summary>
        /// Retrieves only client invoices with the same invoice date or later than the date specified.
        /// </summary>
        public DateTime? InvoiceDateLower { get; set; }
        /// <summary>
        ///  Retrieves only client invoices with the same invoice date or earlier than the date specified.
        /// </summary>
        public DateTime? InvoiceDateUpper { get; set; }
        /// <summary>
        /// If this flag is set to true, no attachments will be returned.
        /// Useful if the invoices have large PDF attachments to limit response time and bandwidth.
        /// The parameter is optional and assumed to be false per default.
        /// </summary>
        public bool? IgnoreFiles { get; set; }

        public Dictionary<string, string> ToQueryStringDictionary()
        {
            return new Dictionary<string, string>
                {
                    {"filter", Status?.ToString().ToLower()},
                    {"query", Query},
                    {"invoiceDateLower", InvoiceDateLower?.ToString("yyyy-MM-dd")},
                    {"invoiceDateUpper", InvoiceDateUpper?.ToString("yyyy-MM-dd")},
                    {"ignoreFiles", IgnoreFiles?.ToString().ToLower()}
                }
                .Where(x => x.Value != null)
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}