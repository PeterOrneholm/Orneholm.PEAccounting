using System.Collections.Generic;
using System.Linq;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
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

        private List<int> LeadUserIds { get; set; }

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

                LeadUserIds = native.leads?.Select(x => x.user.id).ToList() ?? new List<int>(),

                FixedPricePlannedInvoices = native.fixedpriceplannedinvoice?.Select(FixedPricePlannedInvoice.FromNative).ToList() ?? new List<FixedPricePlannedInvoice>(),
                WriteOffs = native.writeoff?.Select(ClientProjectWriteOff.FromNative).ToList() ?? new List<ClientProjectWriteOff>(),

                DimensionEntryId = native.dimensionentry?.id
            };
        }
    }
}