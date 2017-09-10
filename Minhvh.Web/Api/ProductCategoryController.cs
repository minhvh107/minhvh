using AutoMapper;
using Minhvh.Model.Models;
using Minhvh.Service;
using Minhvh.Web.Infrastructure.Core;
using Minhvh.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace Minhvh.Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorCodeService errorCodeService, IProductCategoryService productCategoryService) : base(errorCodeService)
        {
            _productCategoryService = productCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request,string keyword, int pageIndex, int pageSize)
        {
            return CreateHttpReponseMessage(request, () =>
            {
                var model = _productCategoryService.GetAll(keyword);
                var totalRow = model.Count();
                var query = model.OrderByDescending(m => m.CreatedDate).Skip(pageIndex * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);
                var paginationSet = new PaginationSet<ProductCategoryViewModel>
                {
                    Item = responseData,
                    PageIndex = pageIndex,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }
    }
}