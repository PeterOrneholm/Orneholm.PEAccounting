using System;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class ClientInvoiceApproved
    {
        public int UserId { get; set; }
        public DateTime RegistrationDate { get; set; }

        internal static ClientInvoiceApproved FromNative(clientinvoiceApproved native)
        {
            if (native == null)
            {
                return null;
            }

            return new ClientInvoiceApproved
            {
                UserId = native.user.id,
                RegistrationDate = native.registrationdate
            };
        }

        internal clientinvoiceApproved ToNative()
        {
            return new clientinvoiceApproved
            {
                user = new userreference()
                {
                    id = UserId
                },
                registrationdate = RegistrationDate
            };
        }
    }
}