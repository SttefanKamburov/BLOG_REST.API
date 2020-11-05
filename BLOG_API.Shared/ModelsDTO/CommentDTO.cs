using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BLOG_API.Contrains;

namespace BLOG_API.Shared.ModelsDTO
{
    public class CommentDTO : BaseDTO
    {

        [Required]
        [StringLength(AppConstants.NAME_MAX_LRNGHT, MinimumLength = AppConstants.NAME_MIN_LENGHT, ErrorMessage = "Коментарът трябва да е с дължина между 2 и 150 символа")]
        public string Text { get; set; }

        [Required]
        [Range(AppConstants.RATE_MIN_VALUE, AppConstants.RATE_MAX_VALUE, ErrorMessage = "Рейтингът трябва да е със стойност между 1 и 5")]
        public int Rate { get; set; }

        [Required]
        public long PostId { get; set; }
    }
}
