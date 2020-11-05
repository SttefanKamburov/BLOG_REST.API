using System;
using System.Collections.Generic;
using System.Text;

namespace BLOG_API.Shared.InputModels
{
    public class BlogsCriteriaInputModel
    {
        public BlogsCriteriaInputModel() 
        {
            this.Name = "";
            this.PostIds = new List<long>();
            this.UsersIds = new List<long>();
            this.DateCreated = null;        
        }
        public string Name { get; set; }
        public List<long> PostIds { get; set; }
        public List<long> UsersIds { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
