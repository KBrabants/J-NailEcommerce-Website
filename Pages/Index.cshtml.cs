using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyWebSite.Models;
using MyWebSite.Services;

namespace MyWebSite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public JsonFileInfoService InfoServices;

        public IEnumerable<Info> info { get; private set; }


        public IndexModel(ILogger<IndexModel> logger, JsonFileInfoService infoService)
        {
            InfoServices = infoService;
            _logger = logger;
        }

        public void OnGet()
        {
            info = InfoServices.GetInfo();
        }
    }
}