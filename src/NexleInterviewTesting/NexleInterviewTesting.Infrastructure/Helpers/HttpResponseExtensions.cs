using Microsoft.AspNetCore.Http;
using System.Text;

namespace NexleInterviewTesting.Infrastructure.Helpers
{
    public static class HttpResponseExtensions
    {
        /// <summary>
        /// Write a json data to HTTP Response body stream
        /// </summary>
        /// <param name="response"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static async Task WriteJson(this HttpResponse response, string json, Encoding encoding = null)
        {
            encoding ??= Encoding.UTF8;

            await response.WriteAsync(json, encoding);
        }
    }
}
