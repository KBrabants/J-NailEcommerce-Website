using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyWebSite.Models
{
    public class Info
    {
        public int id { get; set; }
        public string Name { get; set; } = "Unkown";
        public string Photo { get; set; } = "https://www.itakeyou.co.uk/idea/wp-content/uploads/2019/11/nail-trends-5.jpg";
        public string ProductPage { get; set; } = "index";
        public int[]? Ratings { get; set; }


        public override string ToString() => JsonSerializer.Serialize<Info>(this);

        public static Info TryGet(int id)
        {
            return new Info();
        }

        public static Info TryGet(string product)
        {
            return null;
        }
    }
}
