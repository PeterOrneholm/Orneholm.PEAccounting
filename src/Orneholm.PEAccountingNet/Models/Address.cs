using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class Address
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        internal static Address FromNative(address native)
        {
            return new Address()
            {
                Address1 = native.address1,
                Address2 = native.address2,
                ZipCode = native.zipcode,
                State = native.state,
                Country = native.country
            };
        }

        internal address ToNative()
        {
            return new address()
            {
                address1 = Address1,
                address2 = Address2,
                zipcode = ZipCode,
                state = State,
                country = Country
            };
        }
    }
}