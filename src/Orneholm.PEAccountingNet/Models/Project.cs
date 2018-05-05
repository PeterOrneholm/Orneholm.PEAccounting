using System;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }

        public static Project FromNative(project native)
        {
            return new Project()
            {
                Id = native.id,
                Name = native.name,
                Description = native.description,
                StartDate = native.startdateSpecified ? (DateTime?)native.startdate : null,
                EndDate = native.enddateSpecified ? (DateTime?)native.enddate : null,
                IsActive = native.active
            };
        }
    }
}