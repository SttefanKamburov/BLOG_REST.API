using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

namespace BLOG_API.DB.Models
{
    [Table("Posts")]
    public class Post : BaseEntity
    {
        public Post()
        {
            this.Comments = new List<Comment>();
        }
        public string Title { get; set; }
        public string Text { get; set; }        
        public int Rate { get; set; }

        [InverseProperty(nameof(Comment.Post))]
        public virtual ICollection<Comment> Comments { get; set; }
        public Blog Blog { get; set; }

        [ForeignKey(nameof(Blog))]
        public long BlogId { get; set; }

    }
}
