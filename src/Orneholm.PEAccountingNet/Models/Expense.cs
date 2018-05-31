using System;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public int Nr { get; set; }
        public DateTime Date { get; set; }
        public CompanyCard CompanyCard { get; set; }
        public long Amount { get; set; }
        public string CurrencyType { get; set; }
        public bool IsReimbursed { get; set; }
        public bool IsPaid { get; set; }
        public int Entries { get; set; }

        internal static Expense FromNative(expensereadablesExpense native)
        {
            return new Expense
            {
                Id = native.id,
                Nr = native.nr,
                Date = native.date,
                CompanyCard = native.companycard != null ? CompanyCard.FromNative(native.companycard) : null,
                Amount = native.amount,
                CurrencyType = native.currencytype,
                IsReimbursed = native.reimbursed,
                IsPaid = native.paid,
                Entries = native.entries
            };
        }
    }
}