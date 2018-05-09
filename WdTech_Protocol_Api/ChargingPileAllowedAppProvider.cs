using System.Collections.Generic;

namespace WdTech_Protocol_Api
{
    public class ChargingPileAllowedAppProvider : IAllowedAppProvider
    {
        private readonly Dictionary<string, HmacAuthenticationService> _hmacAuthenticationServices =
            new Dictionary<string, HmacAuthenticationService>();

        private readonly string _authenticationName;

        public ChargingPileAllowedAppProvider(string authenticationName)
        {
            _authenticationName = authenticationName;
            _hmacAuthenticationServices.Add("", new HmacAuthenticationService());
        }

        public bool IsAllowedApp(string appId)
        {
            if (_hmacAuthenticationServices.ContainsKey(appId)) return true;
            return false;
        }

        public string this[string appId] => _hmacAuthenticationServices[appId].ServiceApiKey;
    }
}