using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class ClientProjectUserActivity
    {
        public int? ActivityId { get; set; }
        public long? Price { get; set; }
        public decimal? DimensionUserPercentage { get; set; }

        public static ClientProjectUserActivity FromNative(clientprojectuseractivity native)
        {
            return new ClientProjectUserActivity
            {
                ActivityId = native.activity?.id,
                Price = native.priceSpecified ? (long?)native.price : null,
                DimensionUserPercentage = native.dimensionuserpercentageSpecified ? (decimal?)native.dimensionuserpercentage : null
            };
        }
    }
}