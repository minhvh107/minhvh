using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minhvh.Data.Infrastructure;
using Minhvh.Data.Repositories;
using Minhvh.Model.Models;

namespace Minhvh.UnitTest.RepositoryTest
{
    [TestClass]
    public class PostCategoryRepositoryTest
    {
        private IDbFactory _dbFactory;
        private IPostCategoryRepository _postCategoryRepository;
        private IUnitOfWork _unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            _dbFactory = new DbFactory();
            _postCategoryRepository = new PostCategoryRepository(_dbFactory);
            _unitOfWork = new UnitOfWork(_dbFactory);
        }

        [TestMethod]
        public void PostCategory_Repository_GetAll()
        {
            var list = _postCategoryRepository.GetAll().ToList();
            Assert.AreEqual(1,list.Count);
        }

        [TestMethod]
        public void PostCategory_Repository_Create()
        {
            PostCategory postCategory = new PostCategory
            {
                Name = "Test_category",
                Alias = "Test-category",
                Status = true
            };
            var result = _postCategoryRepository.Add(postCategory);
            _unitOfWork.SaveChanges();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
    }
}