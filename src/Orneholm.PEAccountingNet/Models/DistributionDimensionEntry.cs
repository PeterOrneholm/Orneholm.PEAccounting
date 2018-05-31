using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class DistributionDimensionEntry
    {
        public int Id { get; set; }
        public decimal? Percentage { get; set; }

        internal static DistributionDimensionEntry FromNative(dimensionsdistributionDimensionentry native)
        {
            return new DistributionDimensionEntry
            {
                Id = native.id,
                Percentage = native.percentageSpecified ? native.percentage : (decimal?)null
            };
        }

        internal dimensionsdistributionDimensionentry ToNative()
        {
            return new dimensionsdistributionDimensionentry
            {
                id = Id,
                percentage = Percentage.GetValueOrDefault(),
                percentageSpecified = Percentage.HasValue
            };
        }
    }
}