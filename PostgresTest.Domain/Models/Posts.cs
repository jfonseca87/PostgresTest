using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostgresTest.Domain.Models
{
    [Table("posts")]
    public class Posts
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("user_name")]
        public string User_Name { get; set; }

        [Column("publish_date")]
        public DateTime Publish_Date { get; set; }

        public ICollection<Comments> Comments { get; set; }
    }
}
