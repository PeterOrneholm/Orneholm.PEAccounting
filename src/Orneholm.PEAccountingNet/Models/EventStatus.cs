using System;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class EventStatus
    {
        public int RegistrationUserId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public EventStatusType Type { get; set; }

        public static EventStatus FromNative(eventstatus native)
        {
            return new EventStatus
            {
                RegistrationUserId = native.registrationuser.id,
                RegistrationDate = native.registrationdate,
                Type = (EventStatusType)native.type
            };
        }
    }
}