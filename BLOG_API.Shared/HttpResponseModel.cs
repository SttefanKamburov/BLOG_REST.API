using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLOG_API.Shared.Enums;
using BLOG_API.Shared.Models;

namespace BLOG_API.Shared
{
    public class HttpResponseModel<T>
    {
        public HttpResponseModel()
        {
        }
        public HttpResponseModel(T model)
        {
            this.Model = model;
        }
        public HttpResponseModel(IEnumerable<HttpErrorModel> errors)
        {
            this.Status = HTTP_RESPONSE_STATUS.ERROR;
            if (errors == null || !errors.Any())
            {
                this.Message = "Unspecified error!";
            }
            else
            {
                this.Errors = errors;
                this.Message = "One or more errors occured!";
            }
        }
        public HttpResponseModel(HttpErrorModel error)
        {
            if (error != null)
            {
                this.Status = HTTP_RESPONSE_STATUS.ERROR;
                this.Message = error.Error;
                this.Errors = new List<HttpErrorModel> { error };
            }
        }
        public HTTP_RESPONSE_STATUS Status { get; set; }
        public string Message { get; set; }
        public T Model { get; set; }
        public IEnumerable<HttpErrorModel> Errors { get; set; }
    }
}
