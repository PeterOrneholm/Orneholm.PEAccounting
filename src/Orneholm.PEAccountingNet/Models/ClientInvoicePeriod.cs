using System;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class ClientInvoicePeriod
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        internal static ClientInvoicePeriod FromNative(clientinvoicePeriod native)
        {
            if (native == null)
            {
                return null;
            }

            return new ClientInvoicePeriod
            {
                StartDate = native.startdate,
                EndDate = native.enddate
            };
        }

        internal clientinvoicePeriod ToNative()
        {
            return new clientinvoicePeriod
            {
                startdate = StartDate,
                enddate = EndDate
            };
        }
    }
}