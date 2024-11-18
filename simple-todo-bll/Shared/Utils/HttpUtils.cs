using Microsoft.AspNetCore.Mvc;
namespace simple_todo_bll.Shared.Utils
{

    public static class ResponseHelper
    {
        // 2xx Success
        public static OkObjectResult Ok(object? value = null) =>
            new OkObjectResult(value);

        public static CreatedResult Created(string uri, object? value = null) =>
            new CreatedResult(uri, value);

        public static NoContentResult NoContent() =>
            new NoContentResult();

        // 3xx Redirection
        public static RedirectResult Redirect(string url) =>
            new RedirectResult(url);

        public static RedirectResult RedirectPermanent(string url) =>
            new RedirectResult(url, permanent: true);

        // 4xx Client Errors
        public static BadRequestObjectResult BadRequest(object? value = null) =>
            new BadRequestObjectResult(value);

        public static UnauthorizedObjectResult Unauthorized(object? value = null) =>
            new UnauthorizedObjectResult(value);

        public static ObjectResult Forbidden(object value) =>
            new ObjectResult(value) { StatusCode = 403 };

        public static NotFoundObjectResult NotFound(object? value = null) =>
            new NotFoundObjectResult(value);

        public static ConflictObjectResult Conflict(object? value = null) =>
            new ConflictObjectResult(value);

        public static UnprocessableEntityObjectResult UnprocessableEntity(object? value = null) =>
            new UnprocessableEntityObjectResult(value);

        // 5xx Server Errors
        public static ObjectResult InternalServerError(object value) =>
            new ObjectResult(value) { StatusCode = 500 };

        public static ObjectResult NotImplemented(object value) =>
            new ObjectResult(value) { StatusCode = 501 };

        public static ObjectResult ServiceUnavailable(object value) =>
            new ObjectResult(value) { StatusCode = 503 };

        public static ObjectResult GatewayTimeout(object value) =>
            new ObjectResult(value) { StatusCode = 504 };

        // Custom Response
        public static ObjectResult CustomResponse(object value, int statusCode) =>
            new ObjectResult(value) { StatusCode = statusCode };
    }


}