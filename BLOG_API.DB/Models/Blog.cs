using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLOG_API.DB.Models
{
    [Table("Blogs")]
   public class Blog : BaseEntity
    {
        public Blog() 
        {
            this.Posts = new List<Post>();
        }
        public string Name { get; set; }

        [InverseProperty(nameof(Post.Blog))]
        public virtual ICollection<Post> Posts { get; set; }      
    }
}
