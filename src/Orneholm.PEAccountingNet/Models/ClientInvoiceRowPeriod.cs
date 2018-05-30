using System;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class ClientInvoiceRowPeriod
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public static ClientInvoiceRowPeriod FromNative(clientinvoicerowPeriod native)
        {
            if (native == null)
            {
                return null;
            }

            return new ClientInvoiceRowPeriod
            {
                StartDate = native.startdate,
                EndDate = native.enddate
            };
        }

        public clientinvoicerowPeriod ToNative()
        {
            return new clientinvoicerowPeriod
            {
                startdate = StartDate,
                enddate = EndDate
            };
        }
    }
}