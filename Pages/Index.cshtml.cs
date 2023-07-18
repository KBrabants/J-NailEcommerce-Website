using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MyWebSite.Models;
using MyWebSite.Services;
using System.Collections.Generic;

namespace MyWebSite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public JsonFileInfoService InfoServices;

        public IEnumerable<Product> Products { get; private set; }


        public IndexModel(ILogger<IndexModel> logger, JsonFileInfoService infoService)
        {
            InfoServices = infoService;
            _logger = logger;
        }

        public void OnGet()
        {
            Products = InfoServices.GetInfo();
        }

    }
}