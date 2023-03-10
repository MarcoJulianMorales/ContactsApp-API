using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Net;

namespace ContactsApp.Models
{
    public class ResponseAPI : ControllerBase
    {

        //Status request HTTP
        public IActionResult Success(dynamic data = null)
        {
            return Result(HttpStatusCode.OK, data);
        }
        public IActionResult Created(dynamic data = null)
        {
            return Result(HttpStatusCode.Created, data);
        }
        public IActionResult Updated(dynamic data = null)
        {
            return Result(HttpStatusCode.Accepted, data);
        }
        public IActionResult BadRequest(dynamic data = null)
        {
            return Result(HttpStatusCode.BadRequest, data);
        }
        public IActionResult Unauthorized(dynamic data = null)
        {
            return Result(HttpStatusCode.Unauthorized, data);
        }
        public IActionResult NotFound(dynamic data = null)
        {
            return Result(HttpStatusCode.NotFound, data);
        }
        public IActionResult InternalError(dynamic data = null)
        {
            return Result(HttpStatusCode.InternalServerError, data);
        }

        //Response type of HTTP Request
        protected IActionResult Result(HttpStatusCode statusCode, dynamic data)
        {
            dynamic result = new ExpandoObject();
            result.Status = statusCode;
            if (data != null)
            {
                result.Data = data;
            }

            return StatusCode(getHttpStatusCodeNumber(statusCode), result);
        }
        private int getHttpStatusCodeNumber(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.OK:
                    return 200;
                case HttpStatusCode.Created:
                    return 201;
                case HttpStatusCode.Accepted:
                    return 202;
                case HttpStatusCode.BadRequest:
                    return 400;
                case HttpStatusCode.Unauthorized:
                    return 401;
                case HttpStatusCode.NotFound:
                    return 404;
                case HttpStatusCode.InternalServerError:
                    return 500;
                default:
                    return 500;
            }
        }
    }
}

