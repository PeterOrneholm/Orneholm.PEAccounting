using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class ClientProjectActivity
    {
        public int? ActivityId { get; set; }

        public long? Price { get; set; }

        public static ClientProjectActivity FromNative(clientprojectactivity native)
        {
            return new ClientProjectActivity
            {
                ActivityId = native.activity?.id,
                Price = native.priceSpecified ? (long?)native.price : null
            };
        }
    }
}