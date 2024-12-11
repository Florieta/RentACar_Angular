using RentACar.Api.Logger;
using System.Net;
using System.Text.Json;

namespace RentACar.WebApi.Middleware
{
    /// <summary>
    /// Exception handler for the whole application.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">delegate.</param>

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Loggs error and returns appropriate response.
        /// </summary>
        /// <param name="context">The Http Context object.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                Log.Instance.LogInformation("Request Sent");
                await _next(context);
                Log.Instance.LogInformation("Response Sent");

            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    KeyNotFoundException => (int)HttpStatusCode.NotFound, // not found error
                    Exception => (int)HttpStatusCode.BadRequest, // custom application error
                    _ => (int)HttpStatusCode.InternalServerError, // unhandled error
                };
                Log.Instance.LogException($"{error}");

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
