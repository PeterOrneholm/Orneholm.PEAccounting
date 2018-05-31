using System;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class DimensionEntry
    {
        public int Id { get; set; }
        public string ForeignId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }

        internal static DimensionEntry FromNative(dimensionentry native)
        {
            return new DimensionEntry
            {
                Id = native.id,
                ForeignId = native.foreignid,
                Name = native.name,
                Description = native.description,
                StartDate = native.startdateSpecified ? native.startdate : (DateTime?)null,
                EndDate = native.enddateSpecified ? native.enddate : (DateTime?)null,
                IsActive = native.active
            };
        }
    }
}