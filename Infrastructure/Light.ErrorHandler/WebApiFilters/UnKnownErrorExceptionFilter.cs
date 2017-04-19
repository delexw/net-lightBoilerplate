using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Threading;
using System.Web.Http.Filters;
using Light.ErrorHandler;
using Light.Domain.Bus.Interfaces;
using System.Net.Http;
using System.Web.Http;
using Light.Event;
using System.Net;
using System.Collections;
using Light.ErrorHandler.Contracts;
using Light.Event.Exceptions;

namespace Light.Security.WebApiFilters
{
    public class UnKnownErrorExceptionFilter : IAutofacExceptionFilter
    {
        private IUnKnownErrorHandler _handler;
        public UnKnownErrorExceptionFilter(IUnKnownErrorHandler handler)
        {
            _handler = handler;
        }

        public Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, 
            CancellationToken cancellationToken)
        {
            if (actionExecutedContext.Exception is NegativeErrorException)
            {
                return Task.FromResult(0);
            }
            return Task.Run(() => _handler.Handle(actionExecutedContext.Exception)).ContinueWith((task)=> {
                //error messages are stored in Exception.Data not Exception.Message
                var une = task.Exception.InnerException as UnKnownErrorException;
                actionExecutedContext.Response =
                    actionExecutedContext.Request.CreateResponse
                            (HttpStatusCode.InternalServerError, une.GetExceptionInfo());
            });
        }
    }
}
