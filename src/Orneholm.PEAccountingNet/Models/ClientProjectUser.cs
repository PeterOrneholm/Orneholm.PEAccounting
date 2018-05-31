using System.Collections.Generic;
using System.Linq;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class ClientProjectUser
    {
        public int UserId { get; set; }

        public List<ClientProjectUserActivity> UserActivities { get; set; }

        internal static ClientProjectUser FromNative(clientprojectuser native)
        {
            return new ClientProjectUser
            {
                UserId = native.user.id,
                UserActivities = native.activity?.Select(ClientProjectUserActivity.FromNative).ToList() ?? new List<ClientProjectUserActivity>()
            };
        }
    }
}