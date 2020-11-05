using System;
using System.Collections.Generic;
using System.Text;

namespace BLOG_API.Shared.InputModels
{
    public class PostsCriteriaInputModel
    {
        public PostsCriteriaInputModel() 
        {
            this.Ids = new List<long>();
            this.Title = "";
            this.Text = "";
            this.RateEquals = null;
            this.RateGreater = null;
            this.RateLower = null;
            this.UserIds = new List<long>();
            this.DateCreated = null;
            this.BlogId = null;
        }
        public List<long> Ids { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int? RateEquals { get; set; }
        public int? RateGreater { get; set; }
        public int? RateLower { get; set; }
        public List<long> UserIds { get; set; }
        public DateTime? DateCreated { get; set; }
        public long? BlogId { get; set; }
    }
}
