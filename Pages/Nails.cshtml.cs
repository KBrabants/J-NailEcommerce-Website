using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyWebSite.Models;
using MyWebSite.Services;
using System.Collections.Generic;
using System.Linq;

namespace MyWebSite.Pages
{
    public class NailsModel : PageModel
    {
        JsonFileInfoService JsonProducts { get; set; }
        public NailsModel(JsonFileInfoService jsonProducts) 
        {
            JsonProducts = jsonProducts;
        }

        [BindProperty(SupportsGet = true)]
        public string PrimaryCategory { get; set; }

        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Product> Matching {  get; set; }
        public void OnGet()
        {
           Products = JsonProducts.GetInfo();

            Matching = from p in Products
                       where p.Categories!.categories.Contains(PrimaryCategory)
                       select p;
        }
    }
}
