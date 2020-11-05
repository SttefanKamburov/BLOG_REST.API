using System;
using System.Collections.Generic;
using System.Text;

namespace BLOG_API.Shared.Models
{
    public class HttpErrorModel
    {
        public HttpErrorModel()
        {
        }
        public HttpErrorModel(string item, string error)
        {
            this.Item = item;
            this.Error = error;
        }
        public string Item { get; set; }
        public string Error { get; set; }
        public override string ToString()
        {
            return $"{this.Item}: {this.Error}";
        }
    }
}
