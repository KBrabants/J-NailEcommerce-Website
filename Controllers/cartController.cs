using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebSite.Models;
using MyWebSite.Services;

namespace MyWebSite.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class cartController : ControllerBase
    {
        public cartController(JsonFileInfoService infoService) 
        {
         InfoService = infoService;
        }

        public JsonFileInfoService InfoService { get; set; }

        [HttpGet]
        public IEnumerable<Info> Get()
        {
            return InfoService.GetInfo();
        }
    }
}
