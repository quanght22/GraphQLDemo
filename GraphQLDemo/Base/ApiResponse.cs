using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GraphQLDemo.Base
{
    public class OkResponse : OkObjectResult
    {
        public OkResponse() : base(new { StatusCode = (int)HttpStatusCode.OK, Result = new { } })
        {
        }

        public OkResponse(object result) : base(new { StatusCode = (int)HttpStatusCode.OK, Result = result })
        {
        }
    }

    public class NotFoundResponse : NotFoundObjectResult
    {
        public NotFoundResponse(string message) : base(new { StatusCode = (int)HttpStatusCode.NotFound, Message = message })
        {
        }
    }

    public class BadRequestResponse : BadRequestObjectResult
    {
        public BadRequestResponse() : base(new { StatusCode = (int)HttpStatusCode.BadRequest, Errors = new string[] { } })
        {
        }

        public BadRequestResponse(ModelStateDictionary modelState) : base(new
        {
            StatusCode = (int)HttpStatusCode.BadRequest,
            Errors = modelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray(),
            Exception = modelState.SelectMany(x => x.Value.Errors).Select(x => x.Exception != null ? x.Exception.ToString() : string.Empty).ToArray(),
        })
        {
        }

        public BadRequestResponse(IEnumerable<string> errors) : base(new { StatusCode = (int)HttpStatusCode.BadRequest, Errors = errors })
        {
        }
    }

    public class ServerErrorResponse : ObjectResult
    {
        public ServerErrorResponse(IEnumerable<string> errors) : base(new { StatusCode = (int)HttpStatusCode.InternalServerError, Errors = errors })
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }

    public class DataConflictErrorResponse : ObjectResult
    {
        public DataConflictErrorResponse() : base(new { StatusCode = (int)HttpStatusCode.InternalServerError, Errors = new List<string>() { "This item has been changed by someone else since you opened it. You will need to refresh it and discard your changes." } })
        {
            StatusCode = (int)HttpStatusCode.PreconditionFailed;
        }
    }
}
