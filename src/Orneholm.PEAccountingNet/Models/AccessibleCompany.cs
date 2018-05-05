using System.Collections.Generic;
using System.Linq;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class AccessibleCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsMain { get; set; }
        public string Token { get; set; }
        private List<AccessType> Accesses { get; set; }

        public static AccessibleCompany FromNative(accessiblecompany native)
        {
            return new AccessibleCompany()
            {
                Id = native.id,
                Name = native.name,
                IsActive = native.active,
                IsMain = native.main,
                Token = native.token,
                Accesses = native.accesses?.ToList().ConvertAll(x => (AccessType)x) ?? new List<AccessType>()
            };
        }
    }
}
