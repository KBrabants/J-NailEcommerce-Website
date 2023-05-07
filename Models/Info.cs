using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyWebSite.Models
{
    public class Info
    {
        public int id { get; set; }
        public string Name { get; set; } = "Unkown";
        public string Photo { get; set; } = "Unkown";
        public string ProductPage { get; set; } = "index";
        public int[]? Ratings { get; set; }

        public override string ToString() => JsonSerializer.Serialize<Info>(this);
    }
}
