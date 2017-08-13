using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minhvh.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private MinhvhDbContext dbContext;

        public MinhvhDbContext Init()
        {
            return dbContext ?? (dbContext = new MinhvhDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
