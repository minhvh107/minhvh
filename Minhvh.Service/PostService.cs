using Minhvh.Data.Infrastructure;
using Minhvh.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Minhvh.Data.Repositories;

namespace Minhvh.Service
{
    public interface IPostService
    {
        void Add(Post post);

        void Update(Post post);

        void Delete(int Id);

        IEnumerable<Post> GetAll();

        IEnumerable<Post> GetAllPagings(int page, int pageSize, out int totalRow);

        Post GetById(int Id);

        IEnumerable<Post> GetAllByTagPaging(string tag,int page, int pageSize, out int totalRow);

        void SaveChanges();
    }

    public class PostService : IPostService
    {
        private IPostRepository _postRepository;
        private IUnitOfWork _unitOfWork;

        public PostService(IPostRepository postRepository,IUnitOfWork unitOfWork)
        {
            this._postRepository = postRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Add(Post post)
        {
            _postRepository.Add(post);
        }

        public void Update(Post post)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id)
        {
            _postRepository.Delete(Id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll( new string[] {"PostCategory"});
        }

        public IEnumerable<Post> GetAllPagings(int page, int pageSize, out int totalRow)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAllPagings(string tag,int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetMultiPaging(m => m.Status, out totalRow, page, pageSize);
        }

        public Post GetById(int Id)
        {
            return _postRepository.GetSingleById(Id);
        }

        public IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAllByTagPaging(int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetMultiPaging(m => m.Status, out totalRow, page, pageSize);
        }

        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }
    }
}