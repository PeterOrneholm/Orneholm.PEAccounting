﻿using System;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class EventCreate
    {
        public string ForeignId { get; set; }

        public int UserId { get; set; }
        public int ActivityId { get; set; }
        public int ClientProjectId { get; set; }

        public string Child { get; set; }
        public DateTime Date { get; set; }
        public decimal Hours { get; set; }
        public string Comment { get; set; }
        public string InternalComment { get; set; }

        public eventwritable ToNative()
        {
            return new eventwritable
            {
                foreignid = ForeignId,
                user = new userreference()
                {
                    id = UserId
                },
                activity = new activityreference()
                {
                    id = ActivityId
                },
                clientproject = new clientprojectreference()
                {
                    id = ClientProjectId
                },
                child = Child,
                date = Date,
                hours = Hours,
                comment = Comment,
                internalcomment = InternalComment
            };
        }
    }
}