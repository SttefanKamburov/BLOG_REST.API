using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BLOG_API.DB.Models;

namespace BLOG_API.Shared.ModelsDTO
{
    public class BaseDTO
    {
        public virtual long Id { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime LastDateModified { get; set; }
        [Required]
        public long? UserCreatorId { get; set; }
        [Required]
        public long? LastUserModifierId { get; set; }
    }
}
