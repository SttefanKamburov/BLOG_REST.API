using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLOG_API.Shared;
using BLOG_API.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BLOG_API
{
    public class BaseController : ControllerBase
    {
        protected HttpResponseModel<T> Error<T>(string item, string message)
        {
            return new HttpResponseModel<T>(new HttpErrorModel(item, message));
        }
        protected HttpResponseModel<T> ModelStateErrors<T>()
        {
            if (this.ModelState == null || this.ModelState.Count == 0)
            {
                return new HttpResponseModel<T>(new HttpErrorModel("Model", "Empty or null model."));
            }
            var errors = new List<HttpErrorModel>();
            foreach (var item in this.ModelState)
            {
                foreach (var error in item.Value.Errors)
                {
                    errors.Add(new HttpErrorModel(item.Key, error.ErrorMessage));
                }
            }
            return new HttpResponseModel<T>(errors);
        }
    }
}
