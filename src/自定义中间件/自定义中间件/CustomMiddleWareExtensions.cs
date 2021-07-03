using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 自定义中间件
{
    public static class CustomMiddleWareExtensions
    {
        public static IApplicationBuilder UseMyMiddleWare(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MyMiddleWare>();
        }
    }
}
