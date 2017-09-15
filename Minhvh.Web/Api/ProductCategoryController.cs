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
using Minhvh.Web.Infrastructure.Extensions;

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
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int pageIndex, int pageSize)
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

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpReponseMessage(request, () =>
            {
                var model = _productCategoryService.GetAll();
                var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("getbyid")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpReponseMessage(request, () =>
            {
                var model = _productCategoryService.GetById(id);

                var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel productCategoryViewModel)
        {
            return CreateHttpReponseMessage(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newProductCategory = new ProductCategory();
                    newProductCategory.UpdateProductCategory(productCategoryViewModel);
                    _productCategoryService.Create(newProductCategory);
                    _productCategoryService.SaveChanges();
                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(newProductCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);

                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductCategoryViewModel productCategoryViewModel)
        {
            return CreateHttpReponseMessage(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newProductCategory = _productCategoryService.GetById(productCategoryViewModel.ID);
                    newProductCategory.UpdateProductCategory(productCategoryViewModel);
                    _productCategoryService.Update(newProductCategory);
                    _productCategoryService.SaveChanges();
                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(newProductCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);

                }
                return response;
            });
        }
    }
}