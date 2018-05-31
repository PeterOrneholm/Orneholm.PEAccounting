using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class Accrual
    {
        public string Month { get; set; }
        public long Sum { get; set; }

        internal static Accrual FromNative(accrual native)
        {
            return new Accrual
            {
                Month = native.month,
                Sum = native.sum
            };
        }

        internal accrual ToNative()
        {
            return new accrual
            {
                month = Month,
                sum = Sum
            };
        }
    }
}