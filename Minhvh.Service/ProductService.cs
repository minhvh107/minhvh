using Minhvh.Data.Infrastructure;
using Minhvh.Data.Repositories;
using Minhvh.Model.Models;
using System.Collections.Generic;
using System;
using System.Security.Cryptography.X509Certificates;
using Minhvh.Common;

namespace Minhvh.Service
{
    public interface IProductService
    {
        Product Create(Product product);

        void Update(Product product);

        Product Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAll(string keyword);

        Product GetById(int id);

        void SaveChanges();
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IProductTagRepository _productTagRepository;
        private readonly ITagRepository _tagRepository;

        public ProductService(
            IProductRepository productRepository,
            IProductTagRepository productTagRepository,
            ITagRepository tagRepository, 
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _productTagRepository = productTagRepository;
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
        }

        public Product Create(Product product)
        {
            var productCreate = _productRepository.Create(product);
            _unitOfWork.SaveChanges();
            //if (!string.IsNullOrEmpty(product.Tags))
            //{
            //    var tags = product.Tags.Split(',');
            //    foreach (string itemTag in tags)
            //    {
            //        var tagId = StringHelper.ToUnsignString(itemTag);
            //        if (_tagRepository.Count(x => x.ID == tagId) == 0)
            //        {
            //            var tagCreate = new Tag
            //            {
            //                ID = tagId,
            //                Name = itemTag,
            //                Type = CommonConstants.ProductTag
            //            };
            //            _tagRepository.Create(tagCreate);
            //        }
            //        var productTags = new ProductTag
            //        {
            //            ProductID = productCreate.ID,
            //            TagID = tagId
            //        };
            //        _productTagRepository.Create(productTags);
            //    }
            //}
            return productCreate;
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);
            //if (!string.IsNullOrEmpty(product.Tags))
            //{
            //    var tags = product.Tags.Split(',');
            //    foreach (string itemTag in tags)
            //    {
            //        var tagId = StringHelper.ToUnsignString(itemTag);
            //        if (_tagRepository.Count(x => x.ID == tagId) == 0)
            //        {
            //            var tagCreate = new Tag
            //            {
            //                ID = tagId,
            //                Name = itemTag,
            //                Type = CommonConstants.ProductTag
            //            };
            //            _tagRepository.Create(tagCreate);
            //        }
            //        _productTagRepository.DeleteMulti(m=>m.ProductID == product.ID);
            //        var productTags = new ProductTag
            //        {
            //            ProductID = product.ID,
            //            TagID = tagId
            //        };
            //        _productTagRepository.Create(productTags);
            //    }
            //}
        }

        public Product Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            //if (!string.IsNullOrEmpty(keyword))
            //    return _productRepository.GetMulti(
            //        m => m.Name.Contains(keyword) || m.Description.Contains(keyword));
            return _productRepository.GetAll();
        }
    }
}