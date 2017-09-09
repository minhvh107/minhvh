using Minhvh.Data.Infrastructure;
using Minhvh.Data.Repositories;
using Minhvh.Model.Models;

namespace Minhvh.Service
{
    public interface IErrorCodeService
    {
        ErrorCode Create(ErrorCode errorCode);

        void SaveChanges();
    }

    public class ErrorCodeService : IErrorCodeService
    {
        private readonly IErrorCodeRepository _errorCodeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ErrorCodeService(IErrorCodeRepository errorCodeRepository, IUnitOfWork unitOfWork)
        {
            _errorCodeRepository = errorCodeRepository;
            _unitOfWork = unitOfWork;
        }

        public ErrorCode Create(ErrorCode errorCode)
        {
            return _errorCodeRepository.Create(errorCode);
        }

        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }
    }
}