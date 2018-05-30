using System;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class ClientInvoiceDiscount
    {
        /// <summary>
        /// In cent.
        /// </summary>
        public long? Absolute { get; set; }
        public decimal? Percentage { get; set; }

        public static ClientInvoiceDiscount AbsoluteDiscount(long discount)
        {
            return new ClientInvoiceDiscount
            {
                Absolute = discount
            };
        }

        public static ClientInvoiceDiscount PercentageDiscount(decimal discount)
        {
            return new ClientInvoiceDiscount
            {
                Percentage = discount
            };
        }


        public static ClientInvoiceDiscount FromNative(clientinvoicerowDiscount native)
        {
            if (native == null)
            {
                return null;
            }

            return new ClientInvoiceDiscount
            {
                Percentage = native.Item as decimal?,
                Absolute = native.Item as long?
            };
        }

        public clientinvoicerowDiscount ToNative()
        {
            if (Absolute.HasValue && Percentage.HasValue)
            {
                throw new ArgumentException("Can't use both Absolute and Percentage discount.");
            }

            return new clientinvoicerowDiscount
            {
                Item = Absolute ?? Percentage
            };
        }
    }
}