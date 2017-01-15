using System.Net;

namespace Promocodoz.SDK.UWP
{
    internal class PromocodozRestClientResponse
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
    }
}
