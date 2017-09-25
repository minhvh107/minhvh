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
    [Authorize]
    public class ProductController : ApiControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IErrorCodeService errorCodeService, IProductService productService) : base(errorCodeService)
        {
            _productService = productService;
        }
        
        [HttpGet]
        [System.Web.Http.AcceptVerbs("Get")]
        public HttpResponseMessage GetAllParents(HttpRequestMessage request)
        {
            return CreateHttpReponseMessage(request, () =>
            {
                var model = _productService.GetAll();
                var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);
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
                var model = _productService.GetById(id);

                var responseData = Mapper.Map<Product, ProductViewModel>(model);

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
                var model = _productService.GetAll(keyword);
                var totalRow = model.Count();
                var query = model.OrderByDescending(m => m.CreatedDate).Skip(pageIndex * pageSize).Take(pageSize);
                var responseData = Mapper.Map<List<Product>, List<ProductViewModel>>(query.ToList());
                var paginationSet = new PaginationSet<ProductViewModel>
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
        public HttpResponseMessage Create(HttpRequestMessage request, ProductViewModel productViewModel)
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
                    productViewModel.CreatedBy = "admin";
                    productViewModel.CreatedDate = DateTime.Now;

                    //Save
                    var newProduct = new Product();
                    newProduct.UpdateProduct(productViewModel);
                    _productService.Create(newProduct);
                    _productService.SaveChanges();

                    //Return
                    var responseData = Mapper.Map<Product, ProductViewModel>(newProduct);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [HttpPut]
        [AllowAnonymous]
        [System.Web.Http.AcceptVerbs("Put")]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductViewModel productVm)
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
                    productVm.UpdatedDate = DateTime.Now;

                    // Update
                    var dbProduct = _productService.GetById(productVm.ID);
                    dbProduct.UpdateProduct(productVm);
                    _productService.Update(dbProduct);
                    _productService.SaveChanges();

                    //Return
                    var responseData = Mapper.Map<Product, ProductViewModel>(dbProduct);
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
                    var oldProduct = _productService.Delete(id);
                    _productService.SaveChanges();

                    //Return
                    var responseData = Mapper.Map<Product, ProductViewModel>(oldProduct);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }

                return response;
            });
        }

        [HttpDelete]
        [AllowAnonymous]
        [System.Web.Http.AcceptVerbs("Delete")]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string listId)
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
                    var lstIds = new JavaScriptSerializer().Deserialize<List<int>>(listId);
                    // Delete
                    foreach (var id in lstIds)
                    {
                        _productService.Delete(id);
                    }
                    _productService.SaveChanges();

                    //Return
                    response = request.CreateResponse(HttpStatusCode.OK, lstIds.Count);
                }

                return response;
            });
        }
    }
}