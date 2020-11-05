using System;
using System.Collections.Generic;
using System.Text;

namespace BLOG_API.Shared.InputModels
{
    public class UsersCriteriaInputModel
    {
        public UsersCriteriaInputModel() 
        {
            this.UserIds = new List<long>();
            this.DateCreated = null;
            this.DateLastModified = null;
            this.UserCreatorId = null;
            this.Username = "";
            this.Email = "";
            this.Name = "";
            this.BlogIds = new List<long>();
            this.CommentIds = new List<long>();
            this.PostIds = new List<long>();
        }
        public List<long> UserIds { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public long? UserCreatorId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public List<long> BlogIds { get; set; }
        public List<long> CommentIds { get; set; }
        public List<long> PostIds { get; set; }
    }
}
