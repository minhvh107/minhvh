using System;

namespace Minhvh.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        MinhvhDbContext Init();
    }
}