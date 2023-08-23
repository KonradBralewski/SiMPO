using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Abstraction.Interceptors
{
    public interface IHttpRequestInterceptor
    {
        void InterceptAfterHttpRequest(HttpResponseMessage? responseMessage);
    }
}
