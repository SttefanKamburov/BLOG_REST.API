using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BLOG_API.Contrains;
using BLOG_API.DB.Models;

namespace BLOG_API.Shared.ModelsDTO
{
    public class PostDTO : BaseDTO
    {
        [Required]
        [StringLength(AppConstrains.NAME_MAX_LRNGHT, MinimumLength = AppConstrains.NAME_MIN_LENGHT, ErrorMessage = "Заглавието трябва да е с дължина между 2 и 150 символа")]
        public string Title { get; set; }

        [Required]
        [StringLength(AppConstrains.NAME_MAX_LRNGHT, MinimumLength = AppConstrains.NAME_MIN_LENGHT, ErrorMessage = "Текстът трябва да е с дължина между 2 и 150 символа")]
        public string Text { get; set; }

        [Required]
        [Range(AppConstrains.RATE_MIN_VALUE,AppConstrains.RATE_MAX_VALUE,ErrorMessage ="Рейтингът трябва да е със стойност между 1 и 5")]
        public int Rate { get; set; }

        [Required]
        public long BlogId { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }
}
