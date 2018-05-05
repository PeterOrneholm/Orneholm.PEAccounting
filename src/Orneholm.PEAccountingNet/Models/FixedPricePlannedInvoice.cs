using System;
using System.Collections.Generic;
using System.Linq;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class FixedPricePlannedInvoice
    {
        public DateTime InvoiceDate { get; set; }
        public int? ClientInvoiceId { get; set; }
        public List<FixedPricePlannedInvoiceRow> Rows { get; set; }

        public static FixedPricePlannedInvoice FromNative(fixedpriceplannedinvoice native)
        {
            return new FixedPricePlannedInvoice
            {
                InvoiceDate = native.invoicedate,
                ClientInvoiceId = native.clientinvoice?.id,
                Rows = native.row?.Select(FixedPricePlannedInvoiceRow.FromNative).ToList() ?? new List<FixedPricePlannedInvoiceRow>()
            };
        }
    }
}