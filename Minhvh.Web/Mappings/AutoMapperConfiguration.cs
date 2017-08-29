using AutoMapper;
using Minhvh.Model.Models;
using Minhvh.Web.Models;

namespace Minhvh.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configuaration()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Post, PostViewModel>();
                cfg.CreateMap<PostTag, PostTagViewModel>();
                cfg.CreateMap<PostCategory, PostCategoryViewModel>();
            });
        }
    }
}