using Minhvh.Data.Infrastructure;
using Minhvh.Model.Models;

namespace Minhvh.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order> { }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}