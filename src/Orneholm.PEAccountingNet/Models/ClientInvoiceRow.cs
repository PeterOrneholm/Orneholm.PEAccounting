using System;
using System.Collections.Generic;
using System.Linq;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class ClientInvoiceRow
    {
        /// <summary>
        /// Unique identifier for this row, read only value.
        /// Can be safely ignored when creating or updating client invoices.
        /// </summary>
        public int? Id { get; set; }

        public decimal Quanity { get; set; }
        /// <summary>
        /// In cent.
        /// </summary>
        public long Price { get; set; }
        /// <summary>
        /// Deprecated when writing, always derived from account configuration.
        /// </summary>
        public decimal Vat { get; set; }

        public int ProductId { get; set; }
        public string Unit { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Deprecated when writing, always derived from product reference configuration.
        /// </summary>
        public int AccountNr { get; set; }

        public List<DistributionDimension> Dimensions { get; set; }

        public ClientInvoiceRowPeriod AccrualsPeriod { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public ClientInvoiceDiscount Discount { get; set; }

        public List<Field> Fields { get; set; }

        public List<Accrual> Accruals { get; set; }

        public static ClientInvoiceRow FromNative(clientinvoicerow native)
        {
            return new ClientInvoiceRow
            {
                Id = native.id,
                Quanity = native.quantity,
                Price = native.price,
                Vat = native.vat,
                ProductId = native.product.id,
                Unit = native.unit,
                Description = native.description,
                AccountNr = native.accountnr,
                Dimensions = native.dimensions?.Select(DistributionDimension.FromNative).ToList() ?? new List<DistributionDimension>(),
                AccrualsPeriod = ClientInvoiceRowPeriod.FromNative(native.period),
                DeliveryDate = native.deliverydateSpecified ? native.deliverydate : (DateTime?)null,
                Discount = ClientInvoiceDiscount.FromNative(native.discount),
                Fields = native.fields?.Select(Field.FromNative).ToList() ?? new List<Field>(),
                Accruals = native.accruals?.Select(Accrual.FromNative).ToList() ?? new List<Accrual>()
            };
        }

        public clientinvoicerow ToNative()
        {
            return new clientinvoicerow
            {
                id = Id.GetValueOrDefault(),
                idSpecified = Id.HasValue,
                quantity = Quanity,
                price = Price,
                vat = Vat,
                product = new productreference { id = ProductId },
                unit = Unit,
                description = Description,
                accountnr = AccountNr,
                dimensions = Dimensions?.Select(x => x.ToNative()).ToArray(),
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