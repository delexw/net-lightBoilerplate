using Light.ErrorHandler.Contracts;
using Light.Event.Exceptions;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http;
using System.Net.Http;
using System.Web.Mvc;
using System.Collections;

namespace Light.ErrorHandler.WebApiFilters
{
    public class NegativeErrorExceptionFilter : IAutofacExceptionFilter
    {
        private INegativeErrorHandler _handler;
        public NegativeErrorExceptionFilter(INegativeErrorHandler handler)
        {
            _handler = handler;
        }

        public Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext,
            CancellationToken cancellationToken)
        {
            if (!(actionExecutedContext.Exception is NegativeErrorException))
            {
                return Task.FromResult(0);
            }
            return Task.Run(() => {
                var nee = actionExecutedContext.Exception as NegativeErrorException;
                _handler.Handle(nee);
                actionExecutedContext.Response =
                    actionExecutedContext.Request.CreateResponse
                            (HttpStatusCode.BadRequest, nee.GetExceptionInfo());
            });
        }
    }
}
