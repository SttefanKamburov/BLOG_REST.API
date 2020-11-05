using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLOG_API.DB.Models
{
    [Table("Comments")]
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public Post Post { get; set; }
        public int Rate { get; set; }

        [ForeignKey(nameof(Post))]
        public long PostId { get; set; }

    }
}
