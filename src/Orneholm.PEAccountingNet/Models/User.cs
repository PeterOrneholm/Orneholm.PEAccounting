using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class User
    {
        public int Id { get; set; }
        public string ForeignId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public int? DimensionEntryId { get; set; }

        public static User FromNative(user native)
        {
            return new User()
            {
                Id = native.id,
                ForeignId = native.foreignid,
                Name = native.name,
                Email = native.email,
                IsActive = native.active,
                DimensionEntryId = native.dimensionentry?.id
            };
        }
    }

    public class Address
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public static Address FromNative(address native)
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
    }
}