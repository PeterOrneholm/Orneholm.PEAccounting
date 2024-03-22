using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models;
public class Account
{
    public int AccountNr { get; set; }
    public string Description { get; set; }
    public long InBalance { get; set; }
    public long OutBalance { get; set; }
    public int DebitCount { get; set; }
    public int CreditCount { get; set; }
    public bool Disabled { get; set; }

    internal static Account FromNative(accountmetadata native)
    {
        return new Account
        {
            AccountNr = native.accountNr,
            Description = native.description,
            InBalance = native.inbalance,
            OutBalance = native.outbalance,
            DebitCount = native.debitcount,
            CreditCount = native.creditcount,
            Disabled = native.disabled,
        };
    }
}
