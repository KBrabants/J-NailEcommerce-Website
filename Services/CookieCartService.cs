using Microsoft.AspNetCore.Http;
using MyWebSite.Models;
using System.Text.Json;

namespace MyWebSite.Services
{
    public class CookieCartService
    {
        public float ShippingCost { get; } = 5;
        public float FreeShippingMinimum { get; } = 80f;


        private CookieOptions cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = false
        };
        private NailQueryingService _NailQueryingService;
        public CookieCartService(NailQueryingService nailQueryingService) {
        
            _NailQueryingService = nailQueryingService;
        }
        private ShoppingCart CalculateCartTotalPrice(ShoppingCart cart)
        {
            float totalPrice = 0;
            foreach(NailItem item in cart.NailItems)
            {
                totalPrice += _NailQueryingService.GetProduct(item.ProductId).Price * item.Sets;
            }
            if(totalPrice >= FreeShippingMinimum)
            {
                cart.FreeShipping = true;
            }
            {
                cart.FreeShipping = false;
                totalPrice += ShippingCost;
            }

            cart.TotalPrice = totalPrice;
            cart.ShippingCost = ShippingCost;
            return cart;
        }
        public ShoppingCart GetCart(HttpRequest Request)
        {
            var cartJson = Request.Cookies["CurrentCart"];

            if (cartJson == null)
                return new ShoppingCart();

            var shoppingCart = JsonSerializer.Deserialize<ShoppingCart>(cartJson);

            shoppingCart = CalculateCartTotalPrice (shoppingCart);

            return shoppingCart;
        }

        public void AddToCart(NailItem AddNailItem,HttpRequest Request, HttpResponse Response)
        {
            ShoppingCart currentCart = GetCart(Request);

            currentCart.NailItems.Add(AddNailItem);
            Response.Cookies.Append("CurrentCart", currentCart.ToString(), cookieOptions);
        }

        public void RemoveFromCart(NailItem RemoveNailItem, HttpRequest Request, HttpResponse Response)
        {
            ShoppingCart currentCart = GetCart(Request);
            foreach(var item in currentCart.NailItems)
            {
                if(item == RemoveNailItem)
                {
                    currentCart.NailItems.Remove(item);
                    break;
                }
            }
            Response.Cookies.Append("CurrentCart", currentCart.ToString(), cookieOptions);
        }

        public void RemoveFromCart(int RemoveAtIndex, HttpRequest Request, HttpResponse Response)
        {
            ShoppingCart currentCart = GetCart(Request);
             
            currentCart.NailItems.RemoveAt(RemoveAtIndex);

            Response.Cookies.Append("CurrentCart", currentCart.ToString(), cookieOptions);
        }
    }
}