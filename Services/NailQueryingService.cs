using MyWebSite.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyWebSite.Services
{
    public class NailQueryingService
    {
        public JsonFileInfoService ProductJson { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public NailQueryingService(JsonFileInfoService jsonInfo) 
        {
            ProductJson =  jsonInfo;

            Products = ProductJson.GetInfo();
        }

        public IEnumerable<Product> ContainsCategory(string category)
        {
            if(Products == null)
                return Enumerable.Empty<Product>();

            List<Product> products = new List<Product>();

            foreach (var Product in Products)
            {
                if (Product.Categories!.categories.Contains(category))
                {
                    products.Add(Product);
                }
            }

            return products;
        }

        public Product GetProduct(int productId)
        {
            if (Products == null)
                return null;

            foreach (var Product in Products)
            {
                if (Product.ProductId == productId)
                {
                    return Product;
                }
            }

            return null;
        }

        public Product GetProduct(string productName)
        {
            if (Products == null)
                return null;

            foreach (var Product in Products)
            {
                if (Product.Name.ToLower() == productName.ToLower())
                {
                    return Product;
                }
            }

            return null;
        }

        internal IEnumerable<Product> ContainsCategorys(Product pageProduct)
        {
            if (Products == null)
                return null;

            List<Product> products = new List<Product>();

            foreach (var Product in Products)
            {

                foreach (var cat in pageProduct.Categories.categories)
                {
                    if (Product.Categories.categories.Contains(cat))
                    {
                        products.Add(Product);
                        break;
                    }
                }
            }

            return products;
        }
    }
}
