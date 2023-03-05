using System.Net;

namespace NexleInterviewTesting.Api.Controllers
{
    /// <summary>
    /// Base controller contains common methods to represent response data
    /// </summary>
    public abstract class BaseController: Controller
    {
        /// <summary>
        /// Return 200 OK response without data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        protected IActionResult Succeed<T>(T data = default)
        {
            return Ok(BaseResponse<T>.SucceedResponse(data));
        }

        /// <summary>
        /// Return 200 OK response without data
        /// </summary>
        /// <returns></returns>
        protected IActionResult Succeed()
        {
            return Ok(BaseResponse<object>.SucceedResponse(null));
        }

        /// <summary>
        /// Return success response with data and custom status code
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        protected IActionResult Succeed<T>(T data, int code)
        {
            return StatusCode(code, BaseResponse<T>.SucceedResponse(data, code));
        }

        /// <summary>
        /// Return success response with custom status code
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        protected IActionResult Succeed(int code)
        {
            return StatusCode(code, BaseResponse<object>.SucceedResponse(null, code));
        }

        /// <summary>
        /// Return 400 Bad Request response with error
        /// </summary>
        /// <returns></returns>
        protected IActionResult BadRequest(string error)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, BaseResponse<string>.BadRequest(error));
        }

        /// <summary>
        /// Return 500 Internal Server Error
        /// </summary>
        /// <returns></returns>
        protected IActionResult ServerError(string error)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, BaseResponse<string>.ServerError(error));
        }

        /// <summary>
        /// Return 404 Not Found response
        /// </summary>
        /// <returns></returns>
        protected IActionResult ResourceNotFound()
        {
            return NotFound(BaseResponse<object>.NotFound());
        }
    }
}
