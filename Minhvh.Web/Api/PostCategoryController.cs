using System.Collections.Generic;
using Minhvh.Model.Models;
using Minhvh.Service;
using Minhvh.Web.Infrastructure.Core;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Minhvh.Web.Models;

namespace Minhvh.Web.Api
{
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        private readonly IPostCategoryService _postCategoryService;

        public PostCategoryController(IErrorCodeService errorCodeService, IPostCategoryService postCategoryService) : base(errorCodeService)
        {
            _postCategoryService = postCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage requestMessage)
        {
            return CreateHttpReponseMessage(requestMessage, () =>
            {
                var lstCategory = _postCategoryService.GetAll();

                var lstPostCategoryVm = Mapper.Map<List<PostCategoryViewModel>>(lstCategory);

                _postCategoryService.SaveChanges();
                var response = requestMessage.CreateResponse(HttpStatusCode.OK, lstPostCategoryVm);
                return response;
            });
        }

        public HttpResponseMessage Post(HttpRequestMessage requestMessage, PostCategory postCategory)
        {
            return CreateHttpReponseMessage(requestMessage, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var category = _postCategoryService.Add(postCategory);
                    _postCategoryService.SaveChanges();

                    response = requestMessage.CreateResponse(HttpStatusCode.Created, category);
                }
                else
                {
                    requestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                return response;
            });
        }

        public HttpResponseMessage Put(HttpRequestMessage requestMessage, PostCategory postCategory)
        {
            return CreateHttpReponseMessage(requestMessage, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    _postCategoryService.Update(postCategory);
                    _postCategoryService.SaveChanges();

                    response = requestMessage.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    requestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                return response;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage requestMessage, int id)
        {
            return CreateHttpReponseMessage(requestMessage, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var category = _postCategoryService.Delete(id);
                    _postCategoryService.SaveChanges();

                    response = requestMessage.CreateResponse(HttpStatusCode.Created, category);
                }
                else
                {
                    requestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                return response;
            });
        }
    }
}