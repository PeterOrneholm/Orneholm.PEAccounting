using System;
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
            return new Client()
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
                IsActive = native.activeSpecified ? (bool?)native.active : null
            };
        }
    }

    public class ClientProject
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public int? BrokerClientId { get; set; }
        public bool IsFixedPrice { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Comment { get; set; }

        public decimal BudgetHours { get; set; }
        public long BudgetAmount { get; set; }

        public long IncomingInvoicedAmount { get; set; }
        public long IncomingInvoicedVatAmount { get; set; }

        public bool UseAllActivities { get; set; }
        public List<ClientProjectActivity> Activities { get; set; }

        public bool UseAllUsers { get; set; }
        public List<ClientProjectUser> Users { get; set; }

        private List<int?> LeadUserIds { get; set; }

        private List<FixedPricePlannedInvoice> FixedPricePlannedInvoices { get; set; }
        private List<ClientProjectWriteOff> WriteOffs { get; set; }

        public int? DimensionEntryId { get; set; }

        public static ClientProject FromNative(clientprojectreadable native)
        {
            return new ClientProject
            {
                Id = native.id.id,
                ClientId = native.client?.id,
                BrokerClientId = native.brokerclient?.id,
                IsFixedPrice = native.fixedprice,
                Number = native.number,
                Name = native.name,
                IsActive = native.active,
                Comment = native.comment,

                BudgetHours = native.budgethours,
                BudgetAmount = native.budgetamount,

                IncomingInvoicedAmount = native.incominginvoicedamount,
                IncomingInvoicedVatAmount = native.incominginvoicedvatamount,

                UseAllActivities = native.allactivities,
                Activities = native.activity?.Select(ClientProjectActivity.FromNative).ToList() ?? new List<ClientProjectActivity>(),

                UseAllUsers = native.allusers,
                Users = native.user?.Select(ClientProjectUser.FromNative).ToList() ?? new List<ClientProjectUser>(),

                LeadUserIds = native.leads?.Select(x => x.user?.id).Where(x => x != null).ToList() ?? new List<int?>(),

                FixedPricePlannedInvoices = native.fixedpriceplannedinvoice?.Select(FixedPricePlannedInvoice.FromNative).ToList() ?? new List<FixedPricePlannedInvoice>(),
                WriteOffs = native.writeoff?.Select(ClientProjectWriteOff.FromNative).ToList() ?? new List<ClientProjectWriteOff>(),

                DimensionEntryId = native.dimensionentry?.id
            };
        }
    }

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

    public class FixedPricePlannedInvoiceRow
    {
        public int? ProductId { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public long Price { get; set; }
        public string Description { get; set; }

        public static FixedPricePlannedInvoiceRow FromNative(fixedpriceplannedinvoicerow native)
        {
            return new FixedPricePlannedInvoiceRow
            {
                ProductId = native.product?.id,
                Quantity = native.quantity,
                Unit = native.unit,
                Price = native.price,
                Description = native.description
            };
        }
    }

    public class ClientProjectWriteOff
    {
        public DateTime Date { get; set; }
        public long Amount { get; set; }
        public long AccountingAmount { get; set; }
        public string Description { get; set; }

        public static ClientProjectWriteOff FromNative(clientprojectwriteoff native)
        {
            return new ClientProjectWriteOff
            {
                Date = native.date,
                Amount = native.amount,
                AccountingAmount = native.accountingamount,
                Description = native.description
            };
        }
    }

    public class ClientProjectActivity
    {
        public int? ActivityId { get; set; }

        public long? Price { get; set; }

        public static ClientProjectActivity FromNative(clientprojectactivity native)
        {
            return new ClientProjectActivity
            {
                ActivityId = native.activity?.id,
                Price = native.priceSpecified ? (long?)native.price : null
            };
        }
    }

    public class ClientProjectUser
    {
        public int? UserId { get; set; }

        public List<ClientProjectUserActivity> UserActivities { get; set; }

        public static ClientProjectUser FromNative(clientprojectuser native)
        {
            return new ClientProjectUser
            {
                UserId = native.user?.id,
                UserActivities = native.activity?.Select(ClientProjectUserActivity.FromNative).ToList() ?? new List<ClientProjectUserActivity>()
            };
        }
    }

    public class ClientProjectUserActivity
    {
        public int? ActivityId { get; set; }
        public long? Price { get; set; }
        public decimal? DimensionUserPercentage { get; set; }

        public static ClientProjectUserActivity FromNative(clientprojectuseractivity native)
        {
            return new ClientProjectUserActivity
            {
                ActivityId = native.activity?.id,
                Price = native.priceSpecified ? (long?)native.price : null,
                DimensionUserPercentage = native.dimensionuserpercentageSpecified ? (decimal?)native.dimensionuserpercentage : null
            };
        }
    }
}