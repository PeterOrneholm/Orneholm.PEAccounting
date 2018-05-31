using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class Dimension
    {
        public int Id { get; set; }
        public string ForeignId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        internal static Dimension FromNative(dimension native)
        {
            return new Dimension
            {
                Id = native.id,
                ForeignId = native.foreignid,
                Name = native.name,
                Description = native.description,
                IsActive = native.active
            };
        } 
    }
}