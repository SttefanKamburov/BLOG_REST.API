using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BLOG_API.Contrains;
using BLOG_API.DB.Models;

namespace BLOG_API.Shared.ModelsDTO
{
    public class BlogDTO : BaseDTO
    {
        [Required]
        [StringLength(AppConstants.NAME_MAX_LRNGHT, MinimumLength = AppConstants.NAME_MIN_LENGHT, ErrorMessage = "Името трябва да е с дължина между 2 и 150 символа")]
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
