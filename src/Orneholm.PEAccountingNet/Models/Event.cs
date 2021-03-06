﻿using System;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string ForeignId { get; set; }

        public int UserId { get; set; }
        public int ActivityId { get; set; }
        public int? ClientProjectId { get; set; }

        public string Child { get; set; }
        public DateTime Date { get; set; }
        public decimal Hours { get; set; }
        public string Comment { get; set; }
        public string InternalComment { get; set; }
        public EventStatus Status { get; set; }

        internal static Event FromNative(eventreadable native)
        {
            return new Event
            {
                Id = native.id.id,
                ForeignId = native.foreignid,
                UserId = native.user.id,
                ActivityId = native.activity.id,
                ClientProjectId = native.clientproject?.id ?? null,
                Child = native.child,
                Date = native.date,
                Hours = native.hours,
                Comment = native.comment,
                InternalComment = native.internalcomment,
                Status = native.status != null ? EventStatus.FromNative(native.status) : null
            };
        }
    }
}