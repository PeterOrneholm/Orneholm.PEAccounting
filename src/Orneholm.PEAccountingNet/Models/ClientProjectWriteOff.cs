using System;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
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
}