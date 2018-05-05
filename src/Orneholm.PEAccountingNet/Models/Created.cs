using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class Created
    {
        public int Id { get; set; }

        public static Created FromNative(created native)
        {
            return new Created
            {
                Id = native.id
            };
        }
    }
}