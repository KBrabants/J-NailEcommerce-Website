using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MyWebSite.Middleware
{
    public class RequestLogging
    {
        private ILogger _logger;
        private RequestDelegate _next;
        private IWebHostEnvironment _environment;
        
        public RequestLogging(RequestDelegate next, ILoggerFactory loggerFactory, IWebHostEnvironment env)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLogging>();
            _environment = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {

               _logger.LogInformation($"Request {context.Request.Method},{_environment.WebRootPath + context.Request.Path} => {context.Response.StatusCode}");
                
            }

        }

    }
}
