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
}