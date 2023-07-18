using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyWebSite.Models;
using MyWebSite.Services;
using PayPal.Api;
using System;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private IHttpContextAccessor httpContextAccessor;
        IConfiguration _configuration;
        private Payment payment;
        private CookieCartService CookieCartService;
        private NailQueryingService NailQueryingService;
        public HomeController(IHttpContextAccessor context, IConfiguration iconfiguration, CookieCartService cookieCart, NailQueryingService nailQueryingService)
        {
            _configuration = iconfiguration;
            httpContextAccessor = context;
            CookieCartService = cookieCart;
            NailQueryingService = nailQueryingService;
        }

        public IActionResult PaymentWithPayPal(string Cancel = null, string blogId = "", string PayerID = "", string guid = "")
        {
            var ClientID = _configuration.GetValue<string>("PayPal:Key");
            var ClientSecret = _configuration.GetValue<string>("PayPal:Secret");
            var mode = _configuration.GetValue<string>("PayPal:mode");

            APIContext apiContext = PaypalConfiguration.GetAPIContext(ClientID, ClientSecret, mode);

            try
            {
                string payerId = PayerID;
                if (string.IsNullOrEmpty(payerId))
                {
                    string baseURI = this.Request.Scheme + "://" + this.Request.Host + "/Home/PaymentWithPaypal?";
                    guid = Convert.ToString(new Random().Next(100000));
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, blogId);
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = lnk.href;
                        break;
                        }
                    }
                httpContextAccessor.HttpContext.Session.SetString("payment", createdPayment.id);
                return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var paymentId = httpContextAccessor.HttpContext.Session.GetString("payment");
                    var executedPayment = ExecutePayment(apiContext, payerId, paymentId as string);

                    if(executedPayment.state.ToLower() != "approved")
                    {
                        return RedirectToPage("/PaymentProcessing/Failed");
                    }
                    var blogIds = executedPayment.transactions[0].item_list.items[0].sku;

                    return RedirectToPage("/PaymentProcessing/Success");
                }
            }
            catch (Exception ex)
            {
                return RedirectToPage("/PaymentProcessing/Failed");
            }

            return RedirectToPage("/PaymentProcessing/Success");
        }
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl, string blogId)
        {
            ShoppingCart cart = CookieCartService.GetCart(httpContextAccessor.HttpContext!.Request);

            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            foreach(NailItem item in cart.NailItems)
            {
                var product = NailQueryingService.GetProduct(item.ProductId);

                itemList.items.Add(new Item()
                {
                    name = product.Name,
                    currency = "USD",
                    price = $"{product.Price}",
                    quantity = $"{item.Sets}",
                    sku = $"{item.ProductId}",
                    description = $"Product Details: {item.PressOnLength}, {item.PressOnStyle} Customer Information: L: {item.CustomerLength} W: {item.CustomerWidth}"
                });
            }
            if (!cart.FreeShipping)
            {
                itemList.items.Add(new Item()
                {
                    name = "Shipping Cost",
                    currency = "USD",
                    price = $"{cart.ShippingCost}",
                    quantity = "1",
                    sku = "ShippingCost",
                    description = ""
                });
            }

            var payer = new Payer()
            {
                payment_method = "paypal",
            };

            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            
            var amount = new Amount()
            {
                currency = "USD",
                total = $"{cart.TotalPrice}"
            };
            var transactionList = new List<Transaction>();
            transactionList.Add(new Transaction()
            {
                description = "tramsaction description",
                invoice_number = Guid.NewGuid().ToString(),
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls,
            };

            return this.payment.Create(apiContext);
        }
    }
}
