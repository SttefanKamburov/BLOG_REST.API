using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLOG_API.DB.Models
{
    [Table("Users")]
    public class User 
    {
        public User() 
        {
            this.BlogsCreated = new List<Blog>();
            this.Posts = new List<Post>();
            this.Comments = new List<Comment>();
        }
        public virtual long Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public User UserCreator { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        
        [ForeignKey(nameof(UserCreator))]
        public long? UserCreatorId { get; set; }
        public User UserLastModified { get; set; }

        [ForeignKey(nameof(UserLastModified))]
        public long? UserLastModifiedId { get; set; }

        [InverseProperty(nameof(Blog.UserCreator))]
        public virtual ICollection<Blog> BlogsCreated { get; set; }

        [InverseProperty(nameof(Comment.UserCreator))]
        public virtual ICollection<Comment> Comments { get; set; }

        [InverseProperty(nameof(Post.UserCreator))]
        public virtual ICollection<Post> Posts { get; set; }

    }
}
