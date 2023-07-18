using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MyWebSite.Filters;
using MyWebSite.Models;
using MyWebSite.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyWebSite.Pages
{
    public class CheckoutModel : PageModel
    {
		private readonly ILogger<IndexModel> _logger;
		public CookieCartService CookieCart { get; }
		private NailQueryingService NailQueryingService { get; set; }
		public List<Product> Products { get; set; }
        public ShoppingCart cart { get; set; }
		public bool HasEmptyCart { get; set; } = false;
		public float FreeShippingMinimum;
		public float ShippingCost;
        public CheckoutModel(ILogger<IndexModel> logger, CookieCartService cookieCart, NailQueryingService nailQueryingService)
		{
			_logger = logger;
			CookieCart = cookieCart;
			NailQueryingService = nailQueryingService;
			Products = new List<Product>();
            FreeShippingMinimum = CookieCart.FreeShippingMinimum;
			ShippingCost = CookieCart.ShippingCost;
        }

		[BindProperty]
		public UserBindingModel input { get; set; }

		public void OnGet()
		{
			cart = CookieCart.GetCart(Request);
            if (cart.NailItems == null || cart.NailItems.Count == 0)
            {
                HasEmptyCart = true;
                return;
            }
            foreach (var item in cart.NailItems)
            {
				var product = NailQueryingService.GetProduct(item.ProductId);
                Products.Add(product);
            }
        }
		public IActionResult OnPost()
		{
			//do something
			if(!ModelState.IsValid)
			{
                return Page();
            }
			
            Console.WriteLine(input.fname);
            return RedirectToPage("index");
        }

		[BindProperty]
		public int RemoveIndex { get; set; }

		public IActionResult OnPostRemoveItem()
		{
			CookieCart.RemoveFromCart(RemoveIndex, Request, Response);

            return RedirectToPage("checkout");
        }
	}

	public class UserBindingModel
	{

		[Required(ErrorMessage ="Name Required")]
		[StringLength(100, ErrorMessage = "Maximum length is {1}")]
		public string fname { get; set; }

		[Required(ErrorMessage = "Last Name Required")]
		[StringLength(100, ErrorMessage = "Maximum length is {1}")]
		public string lname { get; set; }

		[Required(ErrorMessage = "Phone # Required")]
		[Phone]
		public string PhoneNumber { get;set; }

		[Required(ErrorMessage = "Email Required")]
		[EmailAddress]
		public string email { get; set; }

		[Required(ErrorMessage = "street Required")]
		public string street { get; set; }

		[Required(ErrorMessage = "City Required")]
		public string city { get; set; }

		[Required(ErrorMessage = "State Required")]
		public string state { get; set; }

		[Required(ErrorMessage = "Zip Code Required")]
		public string zip { get; set; }


	}
}
