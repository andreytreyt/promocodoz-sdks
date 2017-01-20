using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Promocodoz.SDK.UWP
{
    public class PromocodozRestClient
    {
        private const string Url = "http://promocodoz.com/api/code";

        public PromocodozRestClient(string sid, string secret, string code)
        {
            Sid = sid;
            Secret = secret;
            Code = code;
            Platform = "Windows";
        }

        public string Sid { get; protected set; }
        public string Secret { get; protected set; }
        public string Code { get; protected set; }
        public string Platform { get; protected set; }

        public async Task<string> SendCodeAsync()
        {
            var data = new
            {
                Sid,
                Secret,
                Code,
                Platform
            };

            var response = await Task.Run(async () =>
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        return await client.PostAsJsonAsync(Url, data);
                    }
                    catch
                    {
                        return null;
                    }
                }
            });

            if (response == null) return null;

            var promocodozRestClientResponse = new PromocodozRestClientResponse
            {
                Success = false,
                StatusCode = response.StatusCode
            };

            var result = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                promocodozRestClientResponse.Message = JsonConvert.DeserializeObject<ResponseJsonModel>(result).Message;
            }
            else
            {
                promocodozRestClientResponse.Success = true;
                promocodozRestClientResponse.Data = result;
            }

            return JsonConvert.SerializeObject(promocodozRestClientResponse);
        }
    }
}
