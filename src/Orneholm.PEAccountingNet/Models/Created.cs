using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class Created
    {
        public int Id { get; set; }

        internal static Created FromNative(created native)
        {
            return new Created
            {
                Id = native.id
            };
        }
    }
}