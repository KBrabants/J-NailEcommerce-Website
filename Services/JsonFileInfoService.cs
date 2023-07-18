using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using MyWebSite.Models;
using Microsoft.AspNetCore.Hosting;


namespace MyWebSite.Services
{
    public class JsonFileInfoService
    {
        public JsonFileInfoService(IWebHostEnvironment webHostEnvironment) 
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        public string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "Data", "Products.json"); }
        }

        public IEnumerable<Product> GetInfo()
        {
            string json = File.ReadAllText(JsonFileName);
            return JsonSerializer.Deserialize<Product[]>(json);
        }

        public string Categories
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "Data", "Categories.json"); }
        }
        public IEnumerable<string> GetCategories()
        {
            string json = File.ReadAllText(Categories);
            return JsonSerializer.Deserialize<string[]>(json);
        }
    }
}
