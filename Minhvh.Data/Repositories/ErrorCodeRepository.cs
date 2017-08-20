using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minhvh.Data.Infrastructure;
using Minhvh.Model.Models;

namespace Minhvh.Data.Repositories
{
    public interface IErrorCodeRepository : IRepository<ErrorCode>
    {
        
    }
       
    public class ErrorCodeRepository : RepositoryBase<ErrorCode>, IErrorCodeRepository
    {
        public ErrorCodeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
