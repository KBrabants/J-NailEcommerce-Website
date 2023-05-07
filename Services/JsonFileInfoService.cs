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
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "Data", "Information.json"); }
        }

        public IEnumerable<Info> GetInfo() 
        { 
          using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Info[]>(jsonFileReader.ReadToEnd(), new JsonSerializerOptions{PropertyNameCaseInsensitive = true, })!;
            }
        }

        public void AddRating(int productID, int rating)
        {
            var products = GetInfo();
            var quert = products.First(x => x.id == productID);

            if( quert == null ) 
            { 
                quert.Ratings = new int[] { rating };
            }
            else
            {
                var ratings = quert.Ratings.ToList();
                ratings.Add(rating);
                quert.Ratings = ratings.ToArray();
            }
        }

    }
}
