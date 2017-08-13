using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minhvh.Model.Models
{
    [Table("Footers")]
    public class Footer
    {
        [Key]
        [MaxLength(10)]
        public string ID { set; get; }

        [Required]
        public string Content { set; get; }
    }
}