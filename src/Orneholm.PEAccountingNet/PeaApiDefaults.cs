using System;

namespace Orneholm.PEAccountingNet
{
    public class PeaApiDefaults
    {
        public static readonly Uri ProductionApiBaseUrl = new Uri("https://api.accounting.pe/v1");
        public const string AccessTokenHeaderName = "X-Token";
    }
}
