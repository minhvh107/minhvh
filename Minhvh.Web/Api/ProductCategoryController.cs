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
using System.Web.Script.Serialization;
using Minhvh.Web.Infrastructure.Extensions;

namespace Minhvh.Web.Api
{
    public class ProductCategoryController : ApiControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorCodeService errorCodeService, IProductCategoryService productCategoryService) : base(errorCodeService)
        {
            _productCategoryService = productCategoryService;
        }
        
        [HttpGet]
        [System.Web.Http.AcceptVerbs("Get")]
        public HttpResponseMessage GetAllParents(HttpRequestMessage request)
        {
            return CreateHttpReponseMessage(request, () =>
            {
                var model = _productCategoryService.GetAll();
                var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [HttpGet]
        [System.Web.Http.AcceptVerbs("Get")]
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

        [HttpGet]
        [System.Web.Http.AcceptVerbs("Get")]
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

        [HttpPost]
        [AllowAnonymous]
        [System.Web.Http.AcceptVerbs("Post")]
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
                    //Value Default
                    productCategoryViewModel.CreatedBy = "admin";
                    productCategoryViewModel.CreatedDate = DateTime.Now;

                    //Save
                    var newProductCategory = new ProductCategory();
                    newProductCategory.UpdateProductCategory(productCategoryViewModel);
                    _productCategoryService.Create(newProductCategory);
                    _productCategoryService.SaveChanges();

                    //Return
                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(newProductCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [HttpPut]
        [AllowAnonymous]
        [System.Web.Http.AcceptVerbs("Put")]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
        {
            return CreateHttpReponseMessage(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    // Value Default
                    productCategoryVm.UpdatedDate = DateTime.Now;

                    // Update
                    var dbProductCategory = _productCategoryService.GetById(productCategoryVm.ID);
                    dbProductCategory.UpdateProductCategory(productCategoryVm);
                    _productCategoryService.Update(dbProductCategory);
                    _productCategoryService.SaveChanges();

                    //Return
                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(dbProductCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [HttpDelete]
        [AllowAnonymous]
        [System.Web.Http.AcceptVerbs("Delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpReponseMessage(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    // Delete
                    var oldProductCategory = _productCategoryService.Delete(id);
                    _productCategoryService.SaveChanges();

                    //Return
                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(oldProductCategory);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }

                return response;
            });
        }

        [HttpDelete]
        [AllowAnonymous]
        [System.Web.Http.AcceptVerbs("Delete")]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkProductCategories)
        {
            return CreateHttpReponseMessage(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var lstProductCategory = new JavaScriptSerializer().Deserialize<List<ProductCategoryViewModel>>(checkProductCategories);
                    // Delete
                    foreach (var item in lstProductCategory)
                    {
                        _productCategoryService.Delete(item.ID);
                    }
                    _productCategoryService.SaveChanges();

                    //Return
                    response = request.CreateResponse(HttpStatusCode.OK, lstProductCategory.Count);
                }

                return response;
            });
        }
    }
}