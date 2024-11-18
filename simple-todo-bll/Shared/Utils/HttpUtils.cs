using Microsoft.AspNetCore.Mvc;
namespace simple_todo_bll.Shared.Utils
{
    public static class ResponseHelper
    {
        // 2xx Success
        public static IActionResult Ok(object value = null) =>
            value == null ? new OkResult() : new OkObjectResult(value);

        public static IActionResult Created(string uri, object value) =>
            new CreatedResult(uri, value);

        public static IActionResult NoContent() => new NoContentResult();

        // 3xx Redirection
        public static IActionResult Redirect(string url) =>
            new RedirectResult(url);

        public static IActionResult RedirectPermanent(string url) =>
            new RedirectResult(url, permanent: true);

        public static IActionResult RedirectToAction(string actionName, object routeValues = null) =>
            new RedirectToActionResult(actionName, null, routeValues);

        // 4xx Client Errors
        public static IActionResult BadRequest(object value = null) =>
            value == null ? new BadRequestResult() : new BadRequestObjectResult(value);

        public static IActionResult Unauthorized(object value = null) =>
            value == null ? new UnauthorizedResult() : new UnauthorizedObjectResult(value);

        public static IActionResult Forbidden(object value = null) =>
            new ObjectResult(value) { StatusCode = 403 };

        public static IActionResult NotFound(object value = null) =>
            value == null ? new NotFoundResult() : new NotFoundObjectResult(value);

        public static IActionResult Conflict(object value = null) =>
            new ConflictObjectResult(value);

        public static IActionResult UnprocessableEntity(object value = null) =>
            new UnprocessableEntityObjectResult(value);

        // 5xx Server Errors
        public static IActionResult InternalServerError(object value = null) =>
            new ObjectResult(value) { StatusCode = 500 };

        public static IActionResult NotImplemented(object value = null) =>
            new ObjectResult(value) { StatusCode = 501 };

        public static IActionResult ServiceUnavailable(object value = null) =>
            new ObjectResult(value) { StatusCode = 503 };

        public static IActionResult GatewayTimeout(object value = null) =>
            new ObjectResult(value) { StatusCode = 504 };

        // Custom Response
        public static IActionResult CustomResponse(object value, int statusCode) =>
            new ObjectResult(value) { StatusCode = statusCode };
    }

}