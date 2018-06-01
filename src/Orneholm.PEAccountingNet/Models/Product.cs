using Orneholm.PEAccountingNet.Models.Native;
using System.Collections.Generic;
using System.Linq;

namespace Orneholm.PEAccountingNet.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ForeignId { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Optional when creating/updating.
        /// Default value will be used if not set.
        /// </summary>
        public PurchaseType? Type { get; set; }

        public string Unit { get; set; }
        public long Price { get; set; }

        public List<int> DimensionEntriesIds { get; set; }

        /// <summary>
        /// Optional when creating/updating.
        /// Default value will be used if not set.
        /// </summary>
        public int? AccountNr { get; set; }
        /// <summary>
        /// Vat percentage used for domestic sales using this product.
        /// Optional when creating/updating.
        /// Default value will be used if not set.
        /// </summary>
        public decimal? VatPercentage { get; set; }

        internal static Product FromNative(product native)
        {
            return new Product
            {
                Id = native.id,
                ForeignId = native.foreignid,
                Name = native.name,
                Type = (PurchaseType)native.type,
                Unit = native.unit,
                Price = native.price,
                DimensionEntriesIds = native.dimensionentries?.Select(x => x.id).ToList() ?? new List<int>(),
                AccountNr = native.accountnr,
                VatPercentage = native.vatpercentage
            };
        }
    }
}