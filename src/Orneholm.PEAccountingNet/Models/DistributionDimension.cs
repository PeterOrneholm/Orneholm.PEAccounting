using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class DistributionDimension
    {
        public int Id { get; set; }
        public decimal? Percentage { get; set; }

        internal static DistributionDimension FromNative(dimensionsdistributionDimensionentry native)
        {
            return new DistributionDimension
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