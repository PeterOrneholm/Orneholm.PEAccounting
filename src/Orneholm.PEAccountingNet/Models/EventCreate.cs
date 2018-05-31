using System;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class EventCreate
    {
        public string ForeignId { get; set; }

        public int UserId { get; set; }
        public int ActivityId { get; set; }
        public int? ClientProjectId { get; set; }

        public string Child { get; set; }
        public DateTime Date { get; set; }
        public decimal Hours { get; set; }
        public string Comment { get; set; }
        public string InternalComment { get; set; }

        internal eventwritable ToNative()
        {
            return new eventwritable
            {
                foreignid = ForeignId ?? string.Empty,
                user = new userreference()
                {
                    id = UserId
                },
                activity = new activityreference()
                {
                    id = ActivityId
                },
                clientproject = ClientProjectId != null ? new clientprojectreference()
                {
                    id = ClientProjectId.Value
                } : null,
                child = Child,
                date = Date,
                hours = Hours,
                comment = Comment ?? string.Empty,
                internalcomment = InternalComment
            };
        }
    }
}