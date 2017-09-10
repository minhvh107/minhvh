using System.Collections.Generic;
using System.Linq;

namespace Minhvh.Web.Infrastructure.Core
{
    public class PaginationSet<T>
    {
        public int PageIndex { set; get; }
        public int Count => Item?.Count() ?? 0;
        public int TotalPages { set; get; }
        public int TotalCount { set; get; }
        public IEnumerable<T> Item { set; get; }
    }
}