using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class ItemCreated
    {
        public int Id { get; set; }

        internal static ItemCreated FromNative(created native)
        {
            return new ItemCreated
            {
                Id = native.id
            };
        }
    }
}