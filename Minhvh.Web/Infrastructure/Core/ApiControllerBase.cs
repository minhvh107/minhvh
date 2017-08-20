using Minhvh.Model.Models;
using Minhvh.Service;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Minhvh.Web.Infrastructure.Core
{
    public class ApiControllerBase : ApiController
    {
        private readonly IErrorCodeService _errorCodeService;

        public ApiControllerBase(IErrorCodeService errorCodeService)
        {
            _errorCodeService = errorCodeService;
        }

        protected HttpResponseMessage CreateHttpReponseMessage(HttpRequestMessage requestMessage,
            Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;
            try
            {
                response = function.Invoke();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity type of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
            }
            catch (DbUpdateException dbex)
            {
                LogError(dbex);
                if (dbex.InnerException != null)
                    response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbex.InnerException.Message);
            }
            catch (Exception e)
            {
                LogError(e);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            return response;
        }

        private void LogError(Exception ex)
        {
            try
            {
                ErrorCode errorCode = new ErrorCode()
                {
                    CreatedDate = DateTime.Now,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                };
                _errorCodeService.Create(errorCode);
                _errorCodeService.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}