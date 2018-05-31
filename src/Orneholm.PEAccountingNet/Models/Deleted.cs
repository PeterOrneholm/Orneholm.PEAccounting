using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class Deleted
    {
        public int? Id { get; set; }

        internal static Deleted FromNative(deleted native)
        {
            return new Deleted
            {
                Id = native.idSpecified ? (int?)native.id : null
            };
        }
    }
}