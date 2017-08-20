using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minhvh.Model.Models
{
    [Table("ErrorCodes")]
    public class ErrorCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string Message { set; get; }
        public string StackTrace { set; get; }
        public DateTime CreatedDate { set; get; }
    }
}