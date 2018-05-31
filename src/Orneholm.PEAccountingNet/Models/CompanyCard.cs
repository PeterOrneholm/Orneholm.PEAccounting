using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class CompanyCard
    {
        public int Id { get; set; }
        public string Name { get; set; }

        internal static CompanyCard FromNative(companycardreadable native)
        {
            return new CompanyCard
            {
                Id = native.id,
                Name = native.name
            };
        }
    }
}