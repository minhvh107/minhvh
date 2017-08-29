namespace Minhvh.Web.Models
{
    public class PostTagViewModel
    {
        public int PostID { set; get; }

        public virtual PostViewModel Post { set; get; }

        public string TagID { set; get; }

        public virtual TagViewModel Tag { set; get; }
    }
}