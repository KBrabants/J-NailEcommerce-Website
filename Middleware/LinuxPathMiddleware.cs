using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MyWebSite.Middleware
{
    public class LinuxPathMiddleware
    {
        private ILogger _logger;
        private RequestDelegate _next;
        private IWebHostEnvironment _environment;

        public LinuxPathMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IWebHostEnvironment env)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger< LinuxPathMiddleware>();
            _environment = env;
        }

        public async Task Invoke(HttpContext context)
        {

            _environment.ContentRootPath = "/var/MyApp";
            _environment.WebRootPath = "/var/MyApp";
            await _next(context);

        }

    }
}
