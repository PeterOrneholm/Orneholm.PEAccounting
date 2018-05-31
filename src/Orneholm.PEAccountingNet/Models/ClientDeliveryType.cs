using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class ClientDeliveryType
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; }

        internal static ClientDeliveryType FromNative(clientdeliverytypesClientdeliverytype native)
        {
            return new ClientDeliveryType
            {
                Name = native.name,
                IsDefault = native.@default
            };
        }
    }
}