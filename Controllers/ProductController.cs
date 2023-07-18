using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebSite.Models;
using MyWebSite.Services;
using System.Collections.Generic;

namespace MyWebSite.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public JsonFileInfoService InfoService {get;}

        public ProductController(JsonFileInfoService products) 
        { 
            InfoService = products;
        }


        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return InfoService.GetInfo();
        }

        [Route("Rate")]
        [HttpGet]
        public ActionResult Get([FromQuery]string ProductName,[FromQuery] int Rating)
        {
            return Ok();
        }

    }
}
