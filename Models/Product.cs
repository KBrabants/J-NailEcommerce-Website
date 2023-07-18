using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MyWebSite.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public Categories? Categories { get; set; }
        /// <summary>
        /// Should only contain 3 photos, Index 0 Is Cover Photo
        /// </summary>
        public string[] Photos { get; set; } = new string[3];
        public float Price { get; set; }
        public int TotalSold { get; set;} = 0;
    }

    public class Categories
    {
        [Key]
        public int CategoriesId { get; set; }
        public string[] categories { get; set; } = new string[0];
    }

}
