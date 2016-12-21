using System;
using System.Net.Http;

namespace InstagramApi.Helpers
{
    public class HttpHelper
    {
        public static HttpRequestMessage GetDefaultRequest(HttpMethod method, Uri uri)
        {
            var request = new HttpRequestMessage(method, uri);
            return request;
        }
    }
}