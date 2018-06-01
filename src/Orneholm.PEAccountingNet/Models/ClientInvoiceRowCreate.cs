using System;
using System.Collections.Generic;
using System.Linq;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class ClientInvoiceRowCreate
    {
        public decimal Quantity { get; set; }
        /// <summary>
        /// In cent.
        /// </summary>
        public long Price { get; set; }

        public int ProductId { get; set; }
        public string Unit { get; set; }

        public string Description { get; set; }

        public List<DistributionDimensionEntry> DimensionEntries { get; set; }

        public ClientInvoiceRowPeriod AccrualsPeriod { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public ClientInvoiceDiscount Discount { get; set; }

        public List<Field> Fields { get; set; }

        public List<Accrual> Accruals { get; set; }

        internal clientinvoicerow ToNative()
        {
            return new clientinvoicerow
            {
                quantity = Quantity,
                price = Price,
                product = new productreference { id = ProductId },
                unit = Unit ?? string.Empty,
                description = Description ?? string.Empty,
                dimensions = DimensionEntries?.Select(x => x.ToNative()).ToArray(),
                period = AccrualsPeriod?.ToNative(),
                deliverydate = DeliveryDate.GetValueOrDefault(),
                deliverydateSpecified = DeliveryDate.HasValue,
                discount = Discount?.ToNative(),
                fields = Fields?.Select(x => x.ToNative()).ToArray(),
                accruals = Accruals?.Select(x => x.ToNative()).ToArray()
            };
        }
    }
}