using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minhvh.Model.Models
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key]
        [Column(Order = 1)]
        public int OrderID { set; get; }

        [ForeignKey("OrderID")]
        public virtual Order Order { set; get; }

        [Key]
        [Column(Order = 2)]
        public int ProductID { set; get; }

        [ForeignKey("ProductID")]
        public virtual Product Product { set; get; }

        public int Quantity { set; get; }
    }
}