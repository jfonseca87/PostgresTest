using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostgresTest.Domain.Models
{
    [Table("comments")]
    public class Comments
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("comment")]
        public string Comment { get; set; }

        [Column("idpost")]
        public int IdPost { get; set; }

        [Column("user_name")]
        public string User_Name { get; set; }

        [Column("comment_date")]
        public DateTime Comment_Date { get; set; }

        public Posts Post { get; set; }
    }
}
