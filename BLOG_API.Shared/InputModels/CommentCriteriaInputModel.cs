using System;
using System.Collections.Generic;
using System.Text;

namespace BLOG_API.Shared.InputModels
{
    public class CommentCriteriaInputModel
    {
        public CommentCriteriaInputModel() 
        {
            this.CommentIds = new List<long>();
            this.PostIds = new List<long>();
            this.Text = "";
            this.RateEquals = null;
            this.RateLower = null;
            this.RateGreater = null;
            this.UserIds = new List<long>();
            this.DateCreated = null;
            this.BlogId = null;
        }
        public List<long> CommentIds { get; set; }
        public List<long> PostIds { get; set; }
        public string Text { get; set; }
        public int? RateEquals { get; set; }
        public int? RateLower { get; set; }
        public int? RateGreater { get; set; }
        public List<long> UserIds { get; set; }
        public DateTime? DateCreated { get; set; }
        public long? BlogId { get; set; }
    }
}
