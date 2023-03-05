using NexleInterviewTesting.Application.Dto;
using System.Net;
using System.Text.Json;

namespace NexleInterviewTesting.Application.Middleware
{
    public static class ResponseUtils
    {

        public static string GetJsonResponseString(HttpStatusCode statusCode)
        {
            var opts = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var jsonResponse = statusCode switch
            {
                HttpStatusCode.BadRequest => JsonSerializer.Serialize(BaseResponse<object>.BadRequest((string)null), opts),
                HttpStatusCode.Unauthorized => JsonSerializer.Serialize(BaseResponse<object>.Unauthorized(), opts),
                HttpStatusCode.NotFound => JsonSerializer.Serialize(BaseResponse<object>.NotFound(), opts),
                HttpStatusCode.InternalServerError => JsonSerializer.Serialize(BaseResponse<object>.ServerError(), opts),
                _ => JsonSerializer.Serialize(BaseResponse<object>.ServerError(), opts),
            };
            return jsonResponse;
        }

    }
}
