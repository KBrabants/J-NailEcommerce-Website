using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyWebSite.Models;
using MyWebSite.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MyWebSite.Pages
{

    public class NailPageModel : PageModel
    {
        public Product? PageProduct { get; set; }
        public NailQueryingService NailQuerying { get; set; }
        public IEnumerable<Product> SimilarProducts { get; set; }
        CookieCartService cookieCartService { get; set; }
        public NailPageModel(NailQueryingService nailQuerying, CookieCartService cookieService) { 
            NailQuerying = nailQuerying;
            cookieCartService = cookieService;
        }

        public IActionResult OnGet(string product, int Id)
        {
            PageProduct = NailQuerying.GetProduct(product);

            if (PageProduct == null)
            {
                PageProduct = NailQuerying.GetProduct(Id);
            }

            SimilarProducts = NailQuerying.ContainsCategorys(PageProduct);

            if (PageProduct == null) return RedirectToPage("/Index");

            return Page();
        }
        [BindProperty]
        [Required]
        [FromForm]
        public InputModel Input { get; set; } 

        public IActionResult OnPost()
        {
            if(Input.NailItem == null)
            {
                return RedirectToPage("/Index");
            }

            cookieCartService.AddToCart(Input.NailItem!,Request,Response);
            return RedirectToPage("/CheckOut");
        }

        public class InputModel
        {
            [Required]
            public NailItem? NailItem { get; set; }
        }
    }
}
