using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        internal static Activity FromNative(activityreadable native)
        {
            return new Activity
            {
                Id = native.id.id,
                Name = native.name,
                Description = native.description
            };
        }
    }
}