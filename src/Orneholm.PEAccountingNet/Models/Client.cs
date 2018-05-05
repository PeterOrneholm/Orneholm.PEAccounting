using System.Collections.Generic;
using System.Linq;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string ForeignId { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public Address Address { get; set; }
        public string Email { get; set; }
        public string CountryCode { get; set; }
        public int AccountNr { get; set; }
        public int PaymentDays { get; set; }
        public string OrgNo { get; set; }
        public string Phone { get; set; }
        public int? UserId { get; set; }
        public string DeliveryType { get; set; }
        public string VatNr { get; set; }
        public string Gln { get; set; }
        public int? ClientInvoiceTemplateId { get; set; }
        public int? ApproverUserId { get; set; }
        public List<int> DimensionEntriesId { get; set; }
        public bool? IsActive { get; set; }

        public static Client FromNative(client native)
        {
            return new Client
            {
                Id = native.id,
                ForeignId = native.foreignid,
                Name = native.name,
                Contact = native.contact,
                Address = native.address == null ? null : Address.FromNative(native.address),
                Email = native.email,
                CountryCode = native.countrycode,
                AccountNr = native.accountnr,
                PaymentDays = native.paymentdays,
                OrgNo = native.orgno,
                Phone = native.phone,
                UserId = native.user?.id,
                DeliveryType = native.deliverytype,
                VatNr = native.vatnr,
                Gln = native.gln,
                ClientInvoiceTemplateId = native.template?.id,
                ApproverUserId = native.approver?.id,
                DimensionEntriesId = native.dimensionentries?.Select(x => x.id).ToList() ?? new List<int>(),
                IsActive = native.activeSpecified ? (bool?) native.active : null
            };
        }
    }
}