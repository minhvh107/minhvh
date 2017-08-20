using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minhvh.Data.Infrastructure;
using Minhvh.Data.Repositories;
using Minhvh.Model.Models;
using Minhvh.Service;
using Moq;
using System.Collections.Generic;

namespace Minhvh.UnitTest.ServiceTest
{
    [TestClass]
    public class PostCategoryServiceTest
    {
        private Mock<IPostCategoryRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IPostCategoryService _postCategoryService;
        private List<PostCategory> _listCategory;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IPostCategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _postCategoryService = new PostCategoryService(_mockRepository.Object, _mockUnitOfWork.Object);
            _listCategory = new List<PostCategory>()
            {
                new PostCategory{ID = 1,Name = "Danh muc 1",Status = true},
                new PostCategory{ID = 2,Name = "Danh muc 2",Status = true},
                new PostCategory{ID = 3,Name = "Danh muc 3",Status = true}
            };
        }

        [TestMethod]
        public void PostCategory_Service_GetAll()
        {
            //Setup method
            _mockRepository.Setup(m => m.GetAll(null)).Returns(_listCategory);

            // call action
            var result = _postCategoryService.GetAll() as List<PostCategory>;

            // compare
            Assert.IsNotNull(result);
            Assert.AreEqual(3, _listCategory.Count);
        }

        [TestMethod]
        public void PostCategory_Service_Create()
        {
            PostCategory postCategory = new PostCategory()
            {
                Name = "Test",
                Alias = "Test",
                Status =  true

            };
            _mockRepository.Setup(m => m.Add(postCategory)).Returns((PostCategory p) =>
            {
                p.ID = 1;
                return p;
            });
            var results = _postCategoryService.Add(postCategory);
            Assert.IsNotNull(results);
            Assert.AreEqual(1,results.ID);
        }
    }
}