using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using BLOG_API.Contrains;
using BLOG_API.DB.Models;

namespace BLOG_API.Shared.ModelsDTO
{
    public class UserDTO
    {
        public virtual long Id { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        [StringLength(AppConstrains.NAME_MAX_LRNGHT, MinimumLength = AppConstrains.NAME_MIN_LENGHT, ErrorMessage = "Името трябва да е с дължина между 2 и 150 символа")]
        public string Name { get; set; }
        [Required]
        [StringLength(AppConstrains.NAME_MAX_LRNGHT, MinimumLength = AppConstrains.NAME_MIN_LENGHT, ErrorMessage = "Името трябва да е с дължина между 2 и 150 символа")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public long? UserCreatorId { get; set; }
        [Required]
        public long? UserLastModifiedId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        public virtual ICollection<Blog> BlogsCreated { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
