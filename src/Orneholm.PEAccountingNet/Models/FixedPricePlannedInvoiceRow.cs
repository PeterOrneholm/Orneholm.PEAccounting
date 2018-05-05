using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
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
}