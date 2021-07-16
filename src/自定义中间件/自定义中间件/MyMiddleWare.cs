using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace 自定义中间件
{
    public class MyMiddleWare
    {
        private readonly RequestDelegate _next;
        public MyMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            //在这里些中间件的业务代码
            //如果只写业务代码，那这个中间件就是终端中间件，不会指向下一个中间件
            //前面这里是http请求部分的处理
            await httpContext.Response.WriteAsync("MyMiddleWareStart!\r\n");
            await _next(httpContext);
            //后面这里是Http响应部分的处理
            await httpContext.Response.WriteAsync("MyMiddleWareEnd!\r\n");
        }
    }
}
