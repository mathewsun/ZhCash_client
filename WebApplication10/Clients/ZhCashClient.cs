using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using JsonProperty = System.Text.Json.JsonProperty;

namespace WebApplication10.Clients
{
    public class RpcResponseMessage
    {
        public object result { get; set; }
    }

    public class ZhCashClient
    {
        private const string url = "http://127.0.0.1:5555";

        private HttpClient _httpclient;

        public ZhCashClient(HttpClient httpclient)
        {
            _httpclient = httpclient;
        }

        public RpcResponseMessage GetZhCashResponse()
        {
            try
            {
                //Set Basic Auth
                var user = "1";
                var password = "1";
                var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{user}:{password}"));

                var parameters = new Dictionary<string, string> { { "method", "listaddressgroupings" } };
                var encodedContent = new FormUrlEncodedContent(parameters);

                _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);

                //var responseStream = await _httpclient.GetStreamAsync(url);

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new StringContent("{\"method\":\"listaddressgroupings\"}", Encoding.UTF8, "application/json");

                HttpResponseMessage response = _httpclient.Send(requestMessage);

                string apiResponse = response.Content.ReadAsStringAsync().Result;

                try
                {
                    if (apiResponse != "")
                    {
                        RpcResponseMessage result = Newtonsoft.Json.JsonConvert.DeserializeObject<RpcResponseMessage>(apiResponse);

                        return result;
                    }
                    else
                    { throw new Exception(); }
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error ocurred while calling the API. It responded with the following message: {response.StatusCode} {response.ReasonPhrase}");
                }
            }
            catch (Exception e)
            {
                //_logger.LogError(e, $"Something went wrong when calling WeatherStack.com");
                return null;
            }
        }
    }
}
