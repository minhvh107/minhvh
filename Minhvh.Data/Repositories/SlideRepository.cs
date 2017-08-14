using Minhvh.Data.Infrastructure;
using Minhvh.Model.Models;

namespace Minhvh.Data.Repositories
{
    public interface ISlideRepository : IRepository<Slide> { }

    public class SlideRepository : RepositoryBase<Slide>, ISlideRepository
    {
        public SlideRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}