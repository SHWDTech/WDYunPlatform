using System;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WdTech_Protocol_Api
{
    public class HmacAutheResponseDelegateHandler : DelegatingHandler
    {
        private readonly ulong _requestMaxAgeInSeconds;

        private readonly string _authenticationSchema;

        private readonly IAllowedAppProvider _allowedAppProvider;

        public HmacAutheResponseDelegateHandler(ulong maxAge, string schema, IAllowedAppProvider allowedAppProvider)
        {
            _requestMaxAgeInSeconds = maxAge;
            _authenticationSchema = schema;
            _allowedAppProvider = allowedAppProvider;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization != null &&
                _authenticationSchema.Equals(request.Headers.Authorization.Scheme, StringComparison.OrdinalIgnoreCase))
            {
                var rawAuthzHeader = request.Headers.Authorization.Parameter;
                var autherizationHeaderArray = GetAutherizationHeaderValues(rawAuthzHeader);
                if (autherizationHeaderArray != null)
                {
                    var appId = autherizationHeaderArray[0];
                    var incomingBase64Signature = autherizationHeaderArray[1];
                    var nonce = autherizationHeaderArray[2];
                    var requestTimeStamp = autherizationHeaderArray[3];

                    var isValid = IsValidRequestAsync(request, appId, incomingBase64Signature, nonce, requestTimeStamp);

                    if (isValid.Result)
                    {
                        var currentPrincipal = new GenericPrincipal(new GenericIdentity(appId), null);
                        Thread.CurrentPrincipal = currentPrincipal;
                        if (HttpContext.Current != null)
                        {
                            HttpContext.Current.User = currentPrincipal;
                        }
                    }
                    else
                    {
                        var response =
                            request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid Signature");
                        return Task.FromResult(response);
                    }
                }
                else
                {
                    var response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid Header");
                    return Task.FromResult(response);
                }
            }
            else
            {
                var response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid Schema");
                return Task.FromResult(response);
            }
            return base.SendAsync(request, cancellationToken);
        }

        private static string[] GetAutherizationHeaderValues(string rawAuthzHeader)
        {

            var credArray = rawAuthzHeader.Split(':');

            return credArray.Length == 4 ? credArray : null;
        }

        private async Task<bool> IsValidRequestAsync(HttpRequestMessage req, string appId, string incomingBase64Signature, string nonce, string requestTimeStamp)
        {
            var requestContentBase64String = "";
            var requestUri = HttpUtility.UrlEncode(req.RequestUri.AbsoluteUri.ToLower());
            var requestHttpMethod = req.Method.Method;

            if (!_allowedAppProvider.IsAllowedApp(appId))
            {
                return false;
            }

            var sharedKey = _allowedAppProvider[appId];

            if (IsReplayRequest(nonce, requestTimeStamp))
            {
                return false;
            }

            var hash = await ComputeHashAsync(req.Content);

            if (hash != null)
            {
                requestContentBase64String = Convert.ToBase64String(hash);
            }

            var data = $"{appId}{requestHttpMethod}{requestUri}{requestTimeStamp}{nonce}{requestContentBase64String}";

            var secretKeyBytes = Convert.FromBase64String(sharedKey);

            var signature = Encoding.UTF8.GetBytes(data);

            using (var hmac = new HMACSHA256(secretKeyBytes))
            {
                var signatureBytes = hmac.ComputeHash(signature);

                return (incomingBase64Signature.Equals(Convert.ToBase64String(signatureBytes), StringComparison.Ordinal));
            }

        }

        private bool IsReplayRequest(string nonce, string requestTimeStamp)
        {
            if (MemoryCache.Default.Contains(nonce))
            {
                return true;
            }

            var epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            var currentTs = DateTime.UtcNow - epochStart;

            var serverTotalSeconds = Convert.ToUInt64(currentTs.TotalSeconds);
            var requestTotalSeconds = Convert.ToUInt64(requestTimeStamp);

            if ((serverTotalSeconds - requestTotalSeconds) > _requestMaxAgeInSeconds)
            {
                return true;
            }

            MemoryCache.Default.Add(nonce, requestTimeStamp, DateTimeOffset.UtcNow.AddSeconds(_requestMaxAgeInSeconds));

            return false;
        }

        private static async Task<byte[]> ComputeHashAsync(HttpContent httpContent)
        {
            using (var md5 = MD5.Create())
            {
                byte[] hash = null;
                var content = await httpContent.ReadAsByteArrayAsync();
                if (content.Length != 0)
                {
                    hash = md5.ComputeHash(content);
                }
                return hash;
            }
        }
    }

    public interface IAllowedAppProvider
    {
        bool IsAllowedApp(string appId);

        string this[string addId] { get; }
    }
}