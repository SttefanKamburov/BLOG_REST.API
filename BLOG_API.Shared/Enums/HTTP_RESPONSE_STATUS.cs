using System;
using System.Collections.Generic;
using System.Text;

namespace BLOG_API.Shared.Enums
{
  
        public enum HTTP_RESPONSE_STATUS
        {
            NONE = 0,
            SUCCEED = 1,
            TIMEOUT = 2,
            ERROR = 3,
            SESSION_EXPIRED = 4,
        }
    
}
