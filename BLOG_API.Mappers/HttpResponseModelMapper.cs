using System;
using System.Collections.Generic;
using System.Text;
using BLOG_API.Shared;
using BLOG_API.Shared.Enums;

namespace BLOG_API.Mappers
{
    public static class HttpResponseModelMapper
    {
        public static HttpResponseModel<T> MapToHttpResponseModel<T>(this T entity, string message, HTTP_RESPONSE_STATUS status)
        {
            var httpResponseModel = new HttpResponseModel<T>
            {
                Model = entity,
                Message = message,
                Status = status
            };
            return httpResponseModel;
        }
        public static HttpResponseModel<T> MapToHttpResponseModel<T>(this T entity)
        {
            var httpResponseModel = new HttpResponseModel<T>
            {
                Model = entity,
                Message = "SUCCEED",
                Status = HTTP_RESPONSE_STATUS.SUCCEED
            };
            return httpResponseModel;
        }
    }
}
