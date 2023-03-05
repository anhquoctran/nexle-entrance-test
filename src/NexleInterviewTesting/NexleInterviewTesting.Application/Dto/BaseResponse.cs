using System.Net;
using System.Text.Json.Serialization;

namespace NexleInterviewTesting.Application.Dto
{
    public class BaseResponse<T>
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Error { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Dictionary<string, string[]> Errors { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public T Data { get; set; }

        private BaseResponse() { }

        public static BaseResponse<T> SucceedResponse(T data, int statusCode = (int)HttpStatusCode.OK)
        {
            return new BaseResponse<T> { Data = data, Message = "OK", StatusCode = statusCode };
        }

        public static BaseResponse<T> BadRequest(string error, int statusCode = (int)HttpStatusCode.BadRequest)
        {
            return new BaseResponse<T> { Message = "Bad Request", StatusCode = statusCode, Error = error };
        }

        public static BaseResponse<T> BadRequest(Dictionary<string, string[]> error, int statusCode = (int)HttpStatusCode.BadRequest)
        {
            return new BaseResponse<T> { Message = "Bad Request", StatusCode = statusCode, Errors = error };
        }

        public static BaseResponse<T> ServerError(string error = null, int statusCode = (int)HttpStatusCode.InternalServerError)
        {
            return new BaseResponse<T> { Message = "Internal Server Error", StatusCode = statusCode, Error = error };
        }

        public static BaseResponse<object> NotFound()
        {
            return new BaseResponse<object> { Message = "Not Found", StatusCode = (int)HttpStatusCode.NotFound, Error = "Resource Not Found" };
        }

        public static BaseResponse<object> Unauthorized()
        {
            return new BaseResponse<object> { Message = "Unauthorized", StatusCode = (int)HttpStatusCode.Unauthorized, Error = "Unauthorized" };
        }

    }
}
